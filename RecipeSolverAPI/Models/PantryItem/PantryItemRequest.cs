namespace RecipeSolverAPI.Models.PantryItem
{
    public class PantryItemRequest
    {
        

        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        

    }
}
