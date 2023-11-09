using AutoMapper;
using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.PantryItem;
using RecipeSolverAPI.Models.User;



namespace RecipeSolverAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<FoodProduct, FoodProductDto>();
            CreateMap<FoodProduct, FoodProductClass>();

            CreateMap<PantryItem, PantryItemDto>();
            CreateMap<PantryItem, PantryItemClass>();
            
            CreateMap<User, UserDto>();
            CreateMap<User, UserClass>();

         
        }

    }
}