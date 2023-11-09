using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeSolverAPI.Data.DataModels
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? PreparationTime { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool ColdDish { get; set; } = false;
        public int Portions { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public List<IngredientItem> Ingredients { get; set; } = new List<IngredientItem>();

        public TotalNutrition? TotalNutrition { get; set; }

        public List<RecipeRating> Ratings { get; set; } = new List<RecipeRating>();
        public decimal OverallRating { get; set; } = 0;
        public int RatingsNumber { get; set; } = 0;

        public byte[] Photo { get; set; } = Array.Empty<byte>();
    }
}
