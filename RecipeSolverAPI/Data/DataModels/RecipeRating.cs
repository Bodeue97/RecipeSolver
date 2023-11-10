using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeSolverAPI.Data.DataModels
{
    public class RecipeRating
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }
    }
}
