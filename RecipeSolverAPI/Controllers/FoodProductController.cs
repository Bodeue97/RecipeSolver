using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Services.FoodProduct;


namespace RecipeSolverAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodProductController : ControllerBase
    {
        private readonly IFoodProductService _fps;

        public FoodProductController(IFoodProductService fps)
        {
            _fps = fps;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<FoodProductDto>> Create(FoodProductRequest request)
        {
            try
            {
                var createdProduct = await _fps.Create(request);
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<FoodProductDto>> Update(int id, FoodProductRequest request)
        {
            try
            {
                var updatedProduct = await _fps.Update(request, id);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<FoodProductDto>> Get(int id)
        {
            try
            {
                var product = await _fps.Get(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<FoodProductDto>>> GetAll()
        {
            try
            {
                var products = await _fps.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                var deletedProductId = await _fps.Delete(id);
                return Ok(deletedProductId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
