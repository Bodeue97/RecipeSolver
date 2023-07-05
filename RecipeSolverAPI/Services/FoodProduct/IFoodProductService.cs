using RecipeSolverAPI.Models.FoodProduct;

namespace RecipeSolverAPI.Services.FoodProduct
{
    public interface IFoodProductService
    {
        public Task<FoodProductDto> Create(FoodProductCreateRequest request);
        public Task<FoodProductDto> Get(int id);
        public Task<List<FoodProductDto>> GetAll();
        public Task<int> Delete(int id);
        public Task<FoodProductDto> Update(FoodProductUpdateRequest request, int id);
    }
}