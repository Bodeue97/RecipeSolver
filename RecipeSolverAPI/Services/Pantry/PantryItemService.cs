using AutoMapper;
using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Models.PantryItem;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecipeSolverAPI.Services.PantryItem
{

    public class PantryItemService : IPantryItemService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PantryItemService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PantryItemDto> Create(PantryItemRequest item)
        {
            try
            {
                Data.DataModels.User user = await _context.Users.FirstOrDefaultAsync(i => i.Id == item.UserId) ??
                    throw new Exception("User not found");
                Data.DataModels.FoodProduct foodProduct = await _context.FoodProducts.FirstOrDefaultAsync(i => i.Id == item.ProductId) ??
                    throw new Exception("Food product not found");

                Data.DataModels.PantryItem pantryItem = new()
                {
                    ProductId = item.ProductId,
                    Product = foodProduct,
                    UserId = user.Id,
                    User = user,
                    Quantity = item.Quantity

                };
                await _context.AddAsync(pantryItem);
                await _context.SaveChangesAsync();

                return _mapper.Map<PantryItemDto>(pantryItem);



            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var pantryitem = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ??
                    throw new Exception("Pantry item not found");
                _context.PantryItems.Remove(pantryitem);
                await _context.SaveChangesAsync();
                return id;

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<PantryItemDto> Get(int id)
        {
            try
            {
                var pantryItem = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ??
                    throw new Exception("Pantry item not found");

                return _mapper.Map<PantryItemDto>(pantryItem);

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<List<PantryItemDto>> GetUsersItems(int userId)
        {
            try
            {
                var pantryItems = await _context.PantryItems
                    .Where(item => item.UserId == userId)
                    .Include(item => item.Product)
                    .ToListAsync();

                return _mapper.Map<List<PantryItemDto>>(pantryItems);

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<PantryItemDto> Update(int id, decimal quantity)
        {
            try
            {
                var pantryItem = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ??
                    throw new Exception("Pantry item not found");

                pantryItem.Quantity = quantity;
                _context.PantryItems.Update(pantryItem);
                await _context.SaveChangesAsync();
                return _mapper.Map<PantryItemDto>(pantryItem);



            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
