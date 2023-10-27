using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.Pantry;

namespace RecipeSolverAPI.Models.PantryItem
{
    public class PantryItemClass
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public int? ProductId { get; set; }
        public FoodProductClass? Product { get; set; }
        public int? PantryId { get; set; }
        public PantryClass?  Pantry { get; set; }

    }
}