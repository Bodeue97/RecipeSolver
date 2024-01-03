using RecipeSolverAPI.Models.PantryItem;

namespace RecipeSolverAPI.Services.PantryItem
{
    public interface IPantryItemService
    {
        public Task<List<PantryItemDto>> Get(int id);
        public Task<List<PantryItemDto>> GetUsersItems(int userId, string token);
        public Task<PantryItemDto> Create(PantryItemRequest item);
        public Task<PantryItemDto> Update(int id, PantryItemUpdateRequest quantity);
        public Task<int> Delete(int id);

    }
}
