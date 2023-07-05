using System.ComponentModel.DataAnnotations;

namespace RecipeSolverAPI.Data.DataModels;
public class FoodProduct
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal EnergyValue { get; set; }
    public decimal Protein { get; set; }
    public decimal TotalFat { get; set; }
    public decimal SaturatedFat { get; set; }
    public decimal MonounsaturatedFat { get; set; }
    public decimal PolyunsaturatedFat { get; set; }
    public decimal Cholesterol { get; set; }
    public decimal DigestibleCarbohydrates { get; set; }
    public decimal Glucose { get; set; }
    public decimal Fructose { get; set; }
    public decimal Sucrose { get; set; }
    public decimal Lactose { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal Fiber { get; set; }
    public decimal Sodium { get; set; }
    public decimal Potassium { get; set; }
    public decimal Calcium { get; set; }
    public decimal Phosphorus { get; set; }
    public decimal Magnesium { get; set; }
    public decimal Iron { get; set; }
    public decimal VitaminA { get; set; }
    public decimal BetaCarotene { get; set; }
    public decimal VitaminD { get; set; }
    public decimal VitaminE { get; set; }
    public decimal Thiamine { get; set; }
    public decimal Riboflavin { get; set; }
    public decimal Niacin { get; set; }
    public decimal VitaminC { get; set; }

    public List<PantryItem>? PantryItems { get; set; }
}
