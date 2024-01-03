



    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;
    using RecipeSolverAPI.Data.DataModels;

    namespace RecipeSolverAPI.Data
    {
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {
                try
                {
                    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                    if (databaseCreator != null)
                    {
                        if (!databaseCreator.CanConnect()) databaseCreator.Create();
                        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
       


            public DbSet<User> Users => Set<User>();
            public DbSet<FoodProduct> FoodProducts => Set<FoodProduct>();
            public DbSet<PantryItem> PantryItems => Set<PantryItem>();
            public DbSet<IngredientItem> IngredientItems => Set<IngredientItem>();
            public DbSet<Recipe> Recipes => Set<Recipe>();
            public DbSet<RecipeRating> RecipeRating => Set<RecipeRating>();
            public DbSet<TotalNutrition> TotalNutritions => Set<TotalNutrition>();


       

    }
    }