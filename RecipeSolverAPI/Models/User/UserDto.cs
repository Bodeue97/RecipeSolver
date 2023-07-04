using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeSolverAPI.Models.User
{
     public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
       
    }
}