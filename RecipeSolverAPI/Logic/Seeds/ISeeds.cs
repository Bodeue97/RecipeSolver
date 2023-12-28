using RecipeSolverAPI.Data.DataModels;

namespace RecipeSolverAPI.Logic.Seeds;


public interface ISeeds
{
    public Task SeedFoodProducts(DataContext context);
}