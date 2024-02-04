using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.IngredientItem;

namespace RecipeSolverAPI.Models.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? PreparationTime { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool ColdDish { get; set; } = false;
        public int Portions { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public List<Data.DataModels.IngredientItem> Ingredients { get; set; } = new List<Data.DataModels.IngredientItem>();

        public TotalNutrition? TotalNutrition { get; set; }

        public List<RecipeRating> Ratings { get; set; } = new List<RecipeRating>();
        public decimal OverallRating { get; set; }
        public int RatingsNumber { get; set; } = 0;

        public string Photo { get; set; } = string.Empty;
    }
}
