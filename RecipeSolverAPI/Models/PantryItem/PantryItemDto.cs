using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.User;
using System.Text.Json.Serialization;

namespace RecipeSolverAPI.Models.PantryItem
{
    public class PantryItemDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public int? ProductId { get; set; }
        public FoodProductClass? Product { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public UserClass? User { get; set; }

    }
}