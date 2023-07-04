


using Data.DataModels;
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

        // Properties here like: 
        // public DbSet<ClassName> TableName => Set<ClassName>();
        public DbSet<User> Users => Set<User>();
        public DbSet<FoodProduct> FoodProducts => Set<FoodProduct>();
        public DbSet<Pantry> Pantries => Set<Pantry>();
        public DbSet<PantryItem> PantryItems => Set<PantryItem>();


    }
}