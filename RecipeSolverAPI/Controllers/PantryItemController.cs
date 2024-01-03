using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.PantryItem;
using RecipeSolverAPI.Services.PantryItem;

namespace RecipeSolverAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class PantryItemController : ControllerBase
    {
        private readonly IPantryItemService _pis;

        public PantryItemController(IPantryItemService pis)
        {
            _pis = pis;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<PantryItemDto>> Create(PantryItemRequest item)
        {
            try
            {
                return Ok(await _pis.Create(item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<PantryItemDto>> Update(int id, PantryItemUpdateRequest quantity)
        {
            try
            {                
                return Ok(await _pis.Update(id, quantity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<PantryItemDto>> Get(int id)
        {
            try
            {
                return Ok(await _pis.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsersItems/{userId}/{token}")]
        public async Task<ActionResult<List<PantryItemDto>>> GetUsersItems(int userId, string token)
        {
            try
            {
                return Ok(await _pis.GetUsersItems(userId, token));
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
                return Ok(await _pis.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
