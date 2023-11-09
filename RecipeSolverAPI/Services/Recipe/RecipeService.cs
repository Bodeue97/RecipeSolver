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
                Data.DataModels.Recipe newRecipe = new()
                {
                    Title = request.Title,
                    PreparationTime = request.PreparationTime,
                    Category = request.Category,
                    ColdDish = request.ColdDish,
                    Portions = request.Portions,
                    Description = request.Description,

                };
                await _context.Recipes.AddAsync(newRecipe);
                List<Data.DataModels.IngredientItem> ingredients = new();
                foreach(var ingredient in request.Ingredients)
                {
                    Data.DataModels.IngredientItem ingredientItem = new()
                    {
                        Quantity = ingredient.Quantity,
                        Product = await _context.FoodProducts.FirstOrDefaultAsync(i=>i.Id == ingredient.ProductId) ?? throw new Exception("Food product not found"),
                        ProductId = ingredient.ProductId,
                        RecipeId = newRecipe.Id
                    };
                    ingredients.Add(ingredientItem);
                }

                Data.DataModels.TotalNutrition totalNutrition = new TotalNutrition();
                totalNutrition.RecipeId = newRecipe.Id;
                foreach(var ingredient in  ingredients)
                {
                    totalNutrition.EnergyValue += ingredient.Product!.EnergyValue * ingredient.Quantity;
                    totalNutrition.Protein += ingredient.Product.Protein * ingredient.Quantity;
                    totalNutrition.TotalFat += ingredient.Product.TotalFat * ingredient.Quantity;
                    totalNutrition.SaturatedFat += ingredient.Product.SaturatedFat * ingredient.Quantity;
                    totalNutrition.MonounsaturatedFat += ingredient.Product.MonounsaturatedFat * ingredient.Quantity;
                    totalNutrition.PolyunsaturatedFat += ingredient.Product.PolyunsaturatedFat * ingredient.Quantity;
                    totalNutrition.Cholesterol += ingredient.Product.Cholesterol * ingredient.Quantity;
                    totalNutrition.DigestibleCarbohydrates += ingredient.Product.DigestibleCarbohydrates * ingredient.Quantity;
                    totalNutrition.Glucose += ingredient.Product.Glucose * ingredient.Quantity;
                    totalNutrition.Fructose += ingredient.Product.Fructose * ingredient.Quantity;
                    totalNutrition.Sucrose += ingredient.Product.Sucrose * ingredient.Quantity;
                    totalNutrition.Lactose += ingredient.Product.Lactose * ingredient.Quantity;
                    totalNutrition.Fiber += ingredient.Product.Fiber * ingredient.Quantity;
                    totalNutrition.Sodium += ingredient.Product.Sodium * ingredient.Quantity;
                    totalNutrition.Potassium += ingredient.Product.Potassium * ingredient.Quantity;
                    totalNutrition.Calcium += ingredient.Product.Calcium * ingredient.Quantity;
                    totalNutrition.Phosphorus += ingredient.Product.Phosphorus * ingredient.Quantity;
                    totalNutrition.Magnesium += ingredient.Product.Magnesium * ingredient.Quantity;
                    totalNutrition.Iron += ingredient.Product.Iron * ingredient.Quantity;
                    totalNutrition.VitaminA += ingredient.Product.VitaminA * ingredient.Quantity;
                    totalNutrition.BetaCarotene += ingredient.Product.BetaCarotene * ingredient.Quantity;
                    totalNutrition.VitaminD += ingredient.Product.VitaminD * ingredient.Quantity;
                    totalNutrition.VitaminE += ingredient.Product.VitaminE * ingredient.Quantity;
                    totalNutrition.Thiamine += ingredient.Product.Thiamine * ingredient.Quantity;
                    totalNutrition.Riboflavin += ingredient.Product.Riboflavin * ingredient.Quantity;
                    totalNutrition.Niacin += ingredient.Product.Niacin * ingredient.Quantity;
                    totalNutrition.VitaminC += ingredient.Product.VitaminC * ingredient.Quantity;
                }
                await _context.TotalNutritions.AddAsync(totalNutrition);
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
                    .Include(i => i.Ingredients)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(i => i.Id == id)
                    ?? throw new Exception("Recipe not found");

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

                Data.DataModels.Recipe recipe = await _context.Recipes.FirstOrDefaultAsync(i => i.Id == id) ??
                    throw new Exception("Recipe not found");

                Data.DataModels.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId) ??
                    throw new Exception("User not found");
                Data.DataModels.RecipeRating rating = new()
                {
                    RecipeId = id,
                    UserId = user.Id,
                    Rating = request.Rating,

                };
                if (recipe.Ratings.Any(i => i.UserId == user.Id)) {
                   var ratingToEdit = recipe.Ratings.Find(i=>i.UserId == user.Id);
                    ratingToEdit.Rating = rating.Rating;

                }

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
