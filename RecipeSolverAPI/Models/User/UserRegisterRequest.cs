

using System.ComponentModel.DataAnnotations;

namespace RecipeSolverAPI.Models.User
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Confirm password must match the password")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;
    }
}