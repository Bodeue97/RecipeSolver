using RecipeSolverAPI.Models.PantryItem;

namespace RecipeSolverAPI.Models.User

{
    public class UserClass
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<PantryItemClass>? PantryItems { get; set; } 




    }
}