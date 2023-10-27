using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeSolverAPI.Data.DataModels;
public class PantryItem
{
    [Key]
    public int Id { get; set; }

    // Foreign Key to Pantry
    [ForeignKey("PantryId")]
    public int PantryId { get; set; }
    public Pantry? Pantry { get; set; }

    // Foreign Key to FoodProduct
    [ForeignKey("FoodProductId")]
    public int ProductId { get; set; }
    public FoodProduct? Product { get; set; }

    public decimal Quantity { get; set; }
}
