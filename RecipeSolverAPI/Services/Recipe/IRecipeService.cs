using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.Recipe;

namespace RecipeSolverAPI.Services.Recipe
{
    public interface IRecipeService
    {
        public Task<RecipeDto> Create (RecipeRequest request);
        public Task<RecipeDto> Get(int id);
        public Task<List<RecipeDto>> GetAll();
        public Task<decimal> RateRecipe(int id, RecipeRatingRequest request);
        public Task<int> Delete(int id);
        public Task<List<RecipeDto>> GetUsersRecipes(int userId);


    }
}
