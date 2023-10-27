using RecipeSolverAPI.Models.PantryItem;
using RecipeSolverAPI.Models.User;

namespace RecipeSolverAPI.Models.Pantry
{
    public class PantryDto
    {

        public int Id { get; set; }
        public int? UserId { get; set; }
    }
}