using System.ComponentModel.DataAnnotations;
using Data.DataModels;
using RecipeSolverAPI.Data.DataModels;

public class PantryItem
{
    [Key]
    public int Id { get; set; }

    public int FoodProductId { get; set; }
    public FoodProduct? FoodProduct { get; set; }

    public decimal Quantity { get; set; }

    public int PantryId { get; set; }
    public Pantry? Pantry { get; set; }
}
