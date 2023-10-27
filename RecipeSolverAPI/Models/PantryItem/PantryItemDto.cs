using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.Pantry;

namespace RecipeSolverAPI.Models.PantryItem
{
    public class PantryItemDto
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }
        public int? ProductId { get; set; }
        public int? PantryId { get; set; }

    }
}