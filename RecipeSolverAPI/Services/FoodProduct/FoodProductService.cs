using AutoMapper;
using RecipeSolverAPI.Models.FoodProduct;



namespace RecipeSolverAPI.Services.FoodProduct
{

    public class FoodProductService : IFoodProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FoodProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FoodProductDto> Create(FoodProductRequest request)
        {
            try
            {
                Data.DataModels.FoodProduct newProduct = new()
                {
                    Name = request.Name,
                    Type = request.Type,
                    EnergyValue = request.EnergyValue,
                    Protein = request.Protein,
                    TotalFat = request.TotalFat,
                    SaturatedFat = request.SaturatedFat,
                    MonounsaturatedFat = request.MonounsaturatedFat,
                    PolyunsaturatedFat = request.PolyunsaturatedFat,
                    Cholesterol = request.Cholesterol,
                    DigestibleCarbohydrates = request.DigestibleCarbohydrates,
                    Glucose = request.Glucose,
                    Fructose = request.Fructose,
                    Sucrose = request.Sucrose,
                    Lactose = request.Lactose,
                    Unit = request.Unit,
                    Fiber = request.Fiber,
                    Sodium = request.Sodium,
                    Potassium = request.Potassium,
                    Calcium = request.Calcium,
                    Phosphorus = request.Phosphorus,
                    Magnesium = request.Magnesium,
                    Iron = request.Iron,
                    VitaminA = request.VitaminA,
                    BetaCarotene = request.BetaCarotene,
                    VitaminD = request.VitaminD,
                    VitaminE = request.VitaminE,
                    Thiamine = request.Thiamine,
                    Riboflavin = request.Riboflavin,
                    Niacin = request.Niacin,
                    VitaminC = request.VitaminC
                };

                await _context.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return _mapper.Map<FoodProductDto>(newProduct);
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
                var foodProduct = await _context.FoodProducts.FindAsync(id);
                if (foodProduct == null)
                {
                    throw new Exception("Food product not found");
                }

                _context.FoodProducts.Remove(foodProduct);
                await _context.SaveChangesAsync();

                return foodProduct.Id;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }


        public async Task<FoodProductDto> Get(int id)
        {
            try
            {
                var data = await _context.FoodProducts.FirstOrDefaultAsync(fp => fp.Id.Equals(id))
                ?? throw new Exception("Food product not found");
                return _mapper.Map<FoodProductDto>(data);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<List<FoodProductDto>> GetAll()
        {
            try
            {
                var foodProducts = await _context.FoodProducts.ToListAsync();
                return  _mapper.Map<List<FoodProductDto>>(foodProducts);
             
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<FoodProductDto> Update(FoodProductRequest request, int id)
        {
            try
            {
                var existingProduct = await _context.FoodProducts.FirstOrDefaultAsync(fp => fp.Id.Equals(id))
                ?? throw new Exception("Food product not found");


                existingProduct.Name = request.Name;
                existingProduct.Type = request.Type;
                existingProduct.EnergyValue = request.EnergyValue;
                existingProduct.Protein = request.Protein;
                existingProduct.TotalFat = request.TotalFat;
                existingProduct.SaturatedFat = request.SaturatedFat;
                existingProduct.MonounsaturatedFat = request.MonounsaturatedFat;
                existingProduct.PolyunsaturatedFat = request.PolyunsaturatedFat;
                existingProduct.Cholesterol = request.Cholesterol;
                existingProduct.DigestibleCarbohydrates = request.DigestibleCarbohydrates;
                existingProduct.Glucose = request.Glucose;
                existingProduct.Fructose = request.Fructose;
                existingProduct.Sucrose = request.Sucrose;
                existingProduct.Lactose = request.Lactose;
                existingProduct.Unit = request.Unit;
                existingProduct.Fiber = request.Fiber;
                existingProduct.Sodium = request.Sodium;
                existingProduct.Potassium = request.Potassium;
                existingProduct.Calcium = request.Calcium;
                existingProduct.Phosphorus = request.Phosphorus;
                existingProduct.Magnesium = request.Magnesium;
                existingProduct.Iron = request.Iron;
                existingProduct.VitaminA = request.VitaminA;
                existingProduct.BetaCarotene = request.BetaCarotene;
                existingProduct.VitaminD = request.VitaminD;
                existingProduct.VitaminE = request.VitaminE;
                existingProduct.Thiamine = request.Thiamine;
                existingProduct.Riboflavin = request.Riboflavin;
                existingProduct.Niacin = request.Niacin;
                existingProduct.VitaminC = request.VitaminC;

                _context.FoodProducts.Update(existingProduct);
                await _context.SaveChangesAsync();

                return _mapper.Map<FoodProductDto>(existingProduct);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}