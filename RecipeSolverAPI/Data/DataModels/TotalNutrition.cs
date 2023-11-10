namespace RecipeSolverAPI.Data.DataModels
{
    public class TotalNutrition
    {
        public int Id { get; set; }
        public decimal EnergyValue { get; set; } = 0;
        public decimal Protein { get; set; } = 0;
        public decimal TotalFat { get; set; } = 0;
        public decimal SaturatedFat { get; set; } = 0;
        public decimal MonounsaturatedFat { get; set; } = 0;
        public decimal PolyunsaturatedFat { get; set; } = 0;
        public decimal Cholesterol { get; set; } = 0;
        public decimal DigestibleCarbohydrates { get; set; } = 0;
        public decimal Glucose { get; set; } = 0;
        public decimal Fructose { get; set; } = 0;
        public decimal Sucrose { get; set; } = 0;
        public decimal Lactose { get; set; } = 0;
        public decimal Fiber { get; set; } = 0;
        public decimal Sodium { get; set; } = 0;
        public decimal Potassium { get; set; } = 0;
        public decimal Calcium { get; set; } = 0;
        public decimal Phosphorus { get; set; } = 0;
        public decimal Magnesium { get; set; } = 0;
        public decimal Iron { get; set; } = 0;
        public decimal VitaminA { get; set; } = 0;
        public decimal BetaCarotene { get; set; } = 0;
        public decimal VitaminD { get; set; } = 0;
        public decimal VitaminE { get; set; } = 0;
        public decimal Thiamine { get; set; } = 0;
        public decimal Riboflavin { get; set; } = 0;
        public decimal Niacin { get; set; } = 0;
        public decimal VitaminC { get; set; } = 0;
        public int? RecipeId { get; set; }

    }
}
