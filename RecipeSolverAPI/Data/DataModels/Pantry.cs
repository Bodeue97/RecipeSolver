using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeSolverAPI.Data.DataModels
{
    public class Pantry
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key to User
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        // Navigation property to PantryItems
        [JsonIgnore]
        public List<PantryItem>? Items { get; set; }
    }
}
