

using System.ComponentModel.DataAnnotations;

namespace RecipeSolverAPI.Models.Auth
{
     public class ForgotPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}