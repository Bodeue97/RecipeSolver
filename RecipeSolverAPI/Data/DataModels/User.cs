
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSolverAPI.Data.DataModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Role { get; set; } = "User";
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string Token { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenCreated { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        [ForeignKey("PantryId")]
        public int PantryId { get; set; }
        public Pantry? Pantry { get; set; }
       
    }
}