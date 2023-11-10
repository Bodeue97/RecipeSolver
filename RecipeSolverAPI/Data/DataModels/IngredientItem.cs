namespace RecipeSolverAPI.Data.DataModels
{
    public class IngredientItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public FoodProduct Product { get; set; } = new FoodProduct();
        public int RecipeId { get; set; }
    }
}
