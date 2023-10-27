using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.Pantry;
using RecipeSolverAPI.Models.PantryItem;

namespace RecipeSolverAPI.Services.Pantry
{
    public interface IPantryService
    {
        public Task<PantryDto> GetPantryByUserId(int userId);
        public Task<PantryDto> UpdateItemsList(int userId);

    }
}
