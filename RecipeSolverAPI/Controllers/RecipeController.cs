using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.PantryItem;
using RecipeSolverAPI.Models.Recipe;
using RecipeSolverAPI.Services.Recipe;

namespace RecipeSolverAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _rs;

        public RecipeController(IRecipeService rs)
        {
            _rs = rs;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<RecipeDto>> Create(RecipeRequest request)
        {
            try
            {
                return Ok(await _rs.Create(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<RecipeDto>> Get(int id)
        {
            try
            {
                return Ok(await _rs.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RecipeDto>>> GetAll()
        {
            try
            {
                return Ok(await _rs.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("RateRecipe/{id}")]
        public async Task<ActionResult<decimal>> RateRecipe(int id, RecipeRatingRequest request)
        {
            try
            {
                return Ok(await _rs.RateRecipe(id, request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
