using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.FoodProduct;
using RecipeSolverAPI.Services.FoodProduct;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<FoodProductDto>> CreateFoodProduct(FoodProductCreateRequest request)
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
        public async Task<ActionResult<FoodProductDto>> UpdateFoodProduct(int id, FoodProductUpdateRequest request)
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
        public async Task<ActionResult<FoodProductDto>> GetFoodProduct(int id)
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
        public async Task<ActionResult<List<FoodProductDto>>> GetAllFoodProducts()
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
        public async Task<ActionResult<int>> DeleteFoodProduct(int id)
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
