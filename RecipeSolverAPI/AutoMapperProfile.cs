using AutoMapper;
using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.Pantry;
using RecipeSolverAPI.Models.PantryItem;
using RecipeSolverAPI.Models.User;



namespace RecipeSolverAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add maps here like this
            // CreateMap<fromType, toType>();
            CreateMap<FoodProduct, FoodProductDto>();
            CreateMap<FoodProduct, FoodProductClass>();

            CreateMap<Pantry, PantryDto>();
            CreateMap<Pantry, PantryClass>();

            CreateMap<PantryItem, PantryItemDto>();
            CreateMap<PantryItem, PantryItemClass>();
            
            CreateMap<User, UserDto>();
            CreateMap<User, UserClass>();

         
        }

    }
}