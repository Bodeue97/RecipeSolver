using RecipeSolverAPI.Models.PantryItem;

namespace RecipeSolverAPI.Services.PantryItem
{
    public interface IPantryItemService
    {
        public Task<PantryItemDto> Get(int id);
        public Task<List<PantryItemDto>> GetUsersItems(int userId);
        public Task<PantryItemDto> Create(PantryItemRequest item);
        public Task<PantryItemDto> Update(int id, decimal quantity);
        public Task<int> Delete(int id);

    }
}
