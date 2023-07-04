


using RecipeSolverAPI.Data.DataModels;

namespace RecipeSolverAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        // Properties here like: 
        // public DbSet<ClassName> TableName => Set<ClassName>();
        public DbSet<User> Users => Set<User>();


    }
}