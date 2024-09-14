using Business.Abstract;
using Entity.DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDTO userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.IsSuccess)
            {
                return BadRequest(userExists.Message);
            }
            var registerResult =_authService.Register(userForRegisterDto);
            if (!registerResult.IsSuccess)
            {
                return BadRequest(registerResult.Message);
            }
              
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
        [HttpPost("login")]
        public IActionResult Login(UserForLoginDTO userForLoginDTO)
        {
            var loginResult = _authService.Login(userForLoginDTO);
            if (!loginResult.IsSuccess)
            {
                return BadRequest(loginResult.Message);
            }
            var createToken = _authService.CreateAccessToken(loginResult.Data);
            if (!createToken.IsSuccess)
            {
                return BadRequest(createToken.Message);
            }
            return Ok(createToken.Data);
        }
    }
}
