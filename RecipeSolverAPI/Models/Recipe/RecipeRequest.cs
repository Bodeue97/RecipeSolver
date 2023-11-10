using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.IngredientItem;

namespace RecipeSolverAPI.Models.Recipe
{
    public class RecipeRequest
    {
       
        public string Title { get; set; } = string.Empty;
        public int? PreparationTime { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool ColdDish { get; set; } = false;
        public int Portions { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public List<IngredientItemRequest> Ingredients { get; set; } = new List<IngredientItemRequest>();

       // public decimal OverallRating { get; set; }

       // public byte[] Photo { get; set; } = Array.Empty<byte>();
    }
}
