using AutoMapper;
using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.Recipe;


namespace RecipeSolverAPI.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RecipeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RecipeDto> Create(RecipeRequest request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == request.UserId) ?? throw new Exception("Nie znaleziono użytkownika");
               

                Data.DataModels.Recipe newRecipe = new()
                {
                    Title = request.Title,
                    PreparationTime = request.PreparationTime,
                    Category = request.Category,
                    ColdDish = request.ColdDish,
                    Portions = request.Portions,
                    Description = request.Description,
                    Photo = request.Photo,
                    User = user,
                    UserId = request.UserId,

                    

                };
                // dodaje przepis do kontekstu i zapisuje do bazy
                await _context.Recipes.AddAsync(newRecipe);
                await _context.SaveChangesAsync();

                //tworze pustą liste składników
                List<Data.DataModels.IngredientItem> ingredients = new();

                //z każdego requesta z listy tworze nowy obiekt ingredientitem
                foreach(var ingredient in request.Ingredients)
                {
                    Data.DataModels.IngredientItem ingredientItem = new()
                    {
                        Quantity = ingredient.Quantity,
                        Product = await _context.FoodProducts.FirstOrDefaultAsync(i=>i.Id == ingredient.ProductId) ?? throw new Exception("Food product not found"),
                        ProductId = ingredient.ProductId,
                        RecipeId = newRecipe.Id
                    };
                    await _context.IngredientItems.AddAsync(ingredientItem);
                    



                }

                Data.DataModels.TotalNutrition totalNutrition = new();
                totalNutrition.RecipeId = newRecipe.Id;
                await _context.TotalNutritions.AddAsync(totalNutrition);
                await _context.SaveChangesAsync();
                List<IngredientItem> ingredientItems = await _context.IngredientItems
                    .Include(fp => fp.Product)
                    .ToListAsync();

                foreach (var ingredientToCalculate in ingredientItems)
                {
                    var product = ingredientToCalculate.Product;
                    var quantity = ingredientToCalculate.Quantity;

                    foreach (var property in typeof(Data.DataModels.FoodProduct).GetProperties())
                    {
                        if (property.PropertyType == typeof(decimal))
                        {
                            var value = (decimal)property.GetValue(product)!;
                            var totalProperty = typeof(Data.DataModels.TotalNutrition).GetProperty(property.Name);
                            if (totalProperty != null)
                            {
                                var currentTotalValue = (decimal)totalProperty.GetValue(totalNutrition)!;
                                totalProperty.SetValue(totalNutrition, currentTotalValue + (value * 0.01m * quantity));
                            }
                        }
                    }
                }

                _context.TotalNutritions.Update(totalNutrition);
                await _context.SaveChangesAsync();


                newRecipe.TotalNutrition = totalNutrition;



                await _context.SaveChangesAsync();
                return _mapper.Map<RecipeDto>(newRecipe);


            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<RecipeDto> Get(int id)
        {
            try
            {
                Data.DataModels.Recipe recipe = await _context.Recipes
                    .Include(i => i.TotalNutrition)
                    .Include(r => r.Ratings)
                    .Include(i => i.Ingredients)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(i => i.Id == id)
                    ?? throw new Exception("Nie znaleziono przepisu");

                return _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<List<RecipeDto>> GetAll()
        {
            try
            {
                List<Data.DataModels.Recipe> recipes = await _context.Recipes
                    .Include(i => i.TotalNutrition)
                    .Include(r => r.Ratings)
                    .Include(i => i.Ingredients)                    
                    .ThenInclude(i => i.Product)
                    .ToListAsync()
                    ?? throw new Exception("Recipe not found");

                return _mapper.Map<List<RecipeDto>>(recipes);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<decimal> RateRecipe(int id, RecipeRatingRequest request)
        {
            try
            {
                var userCheck = await _context.Users.FirstOrDefaultAsync(i => i.Id == request.UserId) ?? throw new Exception("User not found");
                Data.DataModels.Recipe recipe = await _context.Recipes
                    .FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("Recipe not found");
                if (await _context.RecipeRating.FirstOrDefaultAsync(i => i.UserId == request.UserId && i.RecipeId == id) != null)
                {
                    RecipeRating? usersRating = await _context.RecipeRating.FirstOrDefaultAsync(i => i.UserId == request.UserId && i.RecipeId == id);
                    usersRating!.Rating = request.Rating;
                    _context.RecipeRating.Update(usersRating!);
                    await _context.SaveChangesAsync();
                }
                else
                {

                    RecipeRating usersRating = new ()
                    {
                        RecipeId = id,
                        UserId = request.UserId,
                        Rating = request.Rating
                    };

                    await _context.RecipeRating.AddAsync(usersRating);
                    await _context.SaveChangesAsync();



                }

                List<RecipeRating> recipesRatings = await _context.RecipeRating.Where(i=> i.RecipeId.Equals(id)).ToListAsync();
                recipe.OverallRating = recipesRatings.Sum(r => r.Rating) / recipesRatings.Count;
                recipe.RatingsNumber = recipesRatings.Count;
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

                return recipe.OverallRating;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var recipeToRemove = await _context.Recipes.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("Nie znaleziono przepisu");
                var totalNutritionToRemove = await _context.TotalNutritions.FirstOrDefaultAsync(i => i.RecipeId == id) ?? throw new Exception("Nie znaleziono wartości odżywczych");
                _context.TotalNutritions.Remove(totalNutritionToRemove);
                _context.Recipes.Remove(recipeToRemove);
                _context.SaveChanges();
                return id;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<List<RecipeDto>> GetUsersRecipes(int userId)
        {
            try
            {
                List<Data.DataModels.Recipe> usersRecipes = await _context.Recipes.Where(i => i.UserId == userId).Include(i => i.TotalNutrition)
                    .Include(r => r.Ratings)
                    .Include(i => i.Ingredients)
                    .ThenInclude(i => i.Product)
                    .ToListAsync()
                    ?? throw new Exception("Błąd przy wyszukiwaniu przepisów użytkownika");

                return _mapper.Map<List<RecipeDto>>(usersRecipes);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }



    }
}
