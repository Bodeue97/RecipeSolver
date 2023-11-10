using RecipeSolverAPI.Models.FoodProduct;

namespace RecipeSolverAPI.Models.IngredientItem
{
    public class IngredientItemClass
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? ProductId { get; set; }
        public FoodProductClass? Product { get; set; }
        public int? RecipeId { get; set; }
    }
}

