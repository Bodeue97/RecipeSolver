using RecipeSolverAPI.Models.PantryItem;

namespace RecipeSolverAPI.Models.Pantry
{
    public class PantryClass
    {
       public int Id { get; set; } 
       public int UserId { get; set; }
       public List<PantryItemClass>? Items { get; set; }
    }
}