

namespace RecipeSolverAPI.Data.DataModels
{
    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Role { get; set; } = "User";
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string Language { get; set; } = "pl";
    }
}