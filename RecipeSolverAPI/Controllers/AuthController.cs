using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSolverAPI.Models.Auth;
using RecipeSolverAPI.Models.User;
using RecipeSolverAPI.Services.Auth;

namespace RecipeSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(UserRegisterRequest request)
        {
            try
            {
                return Ok(await _authService.Register(request));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("User exist."))
                {
                    return StatusCode(405, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginRequest request)
        {
            try
            {
                return Ok(await _authService.Login(request));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Email not verified.") || error.Message.Equals("User not found."))
                {
                    return StatusCode(405, error.Message);
                }
                if (error.Message.Equals("Password is incorrect."))
                {
                    return StatusCode(403, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }
        
        [HttpGet("Verify/{token}")]
        public async Task<ActionResult<UserDto>> Verify(string token)
        {
            try
            {
                return Ok(await _authService.Verify(token));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Invalid token."))
                {
                    return StatusCode(403, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }
        
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                return Ok(await _authService.ForgotPassword(request));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("User not found."))
                {
                    return StatusCode(405, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }

        
        [HttpPatch("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                return Ok(await _authService.ResetPassword(request));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Invalid token."))
                {
                    return StatusCode(403, error.Message);
                }
                if (error.Message.Equals("Token expired."))
                {
                    return StatusCode(405, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }
        
        [HttpPatch("ChangePassword"), Authorize]
        public async Task<ActionResult<string>> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                return Ok(await _authService.ChangePassword(request));
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Problem with Claim in HttpContext."))
                {
                    return StatusCode(401, error.Message);
                }
                if (error.Message.Equals("User not found."))
                {
                    return StatusCode(405, error.Message);
                }
                return StatusCode(500, error.Message);
            }
        }
         [HttpPatch("RefreshToken"), Authorize]
        public async Task<ActionResult<string>> RefreshToken(TokenRequest request)
        {
            try
            {
                return Ok(await _authService.RefreshToken(request));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<UserDto>> Get(int id){
            try{
                return Ok(await _authService.Get(id));
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}