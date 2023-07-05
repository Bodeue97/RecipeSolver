using System.ComponentModel.DataAnnotations;

namespace RecipeSolverAPI.Data.DataModels
{
    public class Pantry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        public List<PantryItem>? PantryItems { get; set; }
    }
}