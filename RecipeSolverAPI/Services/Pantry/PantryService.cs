using AutoMapper;
using RecipeSolverAPI.Models.Pantry;

namespace RecipeSolverAPI.Services.Pantry
{
    public class PantryService : IPantryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PantryService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PantryDto> GetPantryByUserId(int userId)
        {
            try
            {
                var pantry = await _context.Pantries.Include(p=>p.Items).FirstOrDefaultAsync(p => p.UserId == userId) ?? throw new Exception("Pantry not found");

                return _mapper.Map<PantryDto>(pantry);


            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<PantryDto> UpdateItemsList(int userId)
        {
            try
            {
                var pantry = await _context.Pantries.Include(p=>p.Items).FirstOrDefaultAsync(p => p.UserId == userId) ?? throw new Exception("Pantry not found");
                var pantryItemsList = await _context.PantryItems
                    .Where(i => i.PantryId == pantry.Id)
                    .ToListAsync();
                pantry.Items = pantryItemsList;
                await _context.SaveChangesAsync();

                return _mapper.Map<PantryDto>(pantry);


            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
       

    }
}
