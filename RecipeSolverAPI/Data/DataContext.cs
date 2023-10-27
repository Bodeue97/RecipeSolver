



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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Pantry)
                .WithOne(p => p.User)
                .HasForeignKey<Pantry>(p => p.UserId); 

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<User> Users => Set<User>();
        public DbSet<FoodProduct> FoodProducts => Set<FoodProduct>();
        public DbSet<Pantry> Pantries => Set<Pantry>();
        public DbSet<PantryItem> PantryItems => Set<PantryItem>();


    }
}