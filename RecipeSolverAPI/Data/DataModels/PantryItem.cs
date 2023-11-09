using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeSolverAPI.Data.DataModels;
public class PantryItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public FoodProduct? Product { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    public decimal Quantity { get; set; }
}