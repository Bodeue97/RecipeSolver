using AutoMapper;
using RecipeSolverAPI.Data.DataModels;
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
                var pantry = await _context.Pantries.FirstOrDefaultAsync(i => i.Id == item.PantryId) ?? throw new Exception("Pantry not found");
                var product = await _context.FoodProducts.FirstOrDefaultAsync(i => i.Id == item.ProductId) ?? throw new Exception("Food product not found");

                var pantryItemsList = pantry.Items;
                if (pantryItemsList != null)
                {
                    if (pantryItemsList.Any(p => p.Id == item.ProductId))
                    {
                        throw new Exception("This item is already present in your pantry");
                    }
                }
               

                Data.DataModels.PantryItem pantryItem = new ()
                {
                 Quantity = item.Quantity,
                 Pantry = pantry,
                 PantryId = item.PantryId,
                 Product = product,
                 ProductId = item.ProductId
                };
                await _context.PantryItems.AddAsync(pantryItem);
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
                var item = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("PantryItem not found");

                _context.PantryItems.Remove(item);
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
                var item = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("PantryItem not found");


                return _mapper.Map<PantryItemDto>(item);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<List<PantryItemDto>> GetAll()
        {
            try
            {
                var items = await _context.PantryItems.ToListAsync();


                return _mapper.Map<List<PantryItemDto>>(items);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<PantryItemDto> Update(int id, PantryItemRequest item)
        {
            try
            {
                var pantryItem = await _context.PantryItems.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception("PantryItem not found");
                pantryItem.Quantity = item.Quantity;



                return _mapper.Map<PantryItemDto>(pantryItem);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
