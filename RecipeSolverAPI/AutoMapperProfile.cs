using AutoMapper;
using Data.DataModels;
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
            CreateMap<Pantry, Models.FoodProduct.FoodProductClass>();
            CreateMap<Pantry, PantryDto>();
            CreateMap<Pantry, Models.Pantry.PantryClass>();
            CreateMap<PantryItem, PantryItemDto>();
            CreateMap<PantryItem, Models.PantryItem.PantryItemClass>();
            CreateMap<User, UserDto>();
            CreateMap<User, Models.User.UserClass>();
        }

    }
}