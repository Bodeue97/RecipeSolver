using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.Pantry;
using RecipeSolverAPI.Services.Pantry;

namespace RecipeSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantryController : ControllerBase
    {
        private readonly IPantryService _ps;

        public PantryController(IPantryService pantryService)
        {
            _ps = pantryService;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<PantryDto>> Get(int id)
        {
            try
            {
              
                return Ok(await _ps.GetPantryByUserId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<PantryDto>> Update(int id)
        {
            try
            {

                return Ok(await _ps.UpdateItemsList(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
