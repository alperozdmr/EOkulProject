using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAuthController : ControllerBase
    {
        private IAuthService<Student, StudentForLoginDto, StudentForRegisterDto> _authService;
        public StudentAuthController(IAuthService<Student, StudentForLoginDto, StudentForRegisterDto> authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(StudentForLoginDto userForLoginDto)
        {
            var userToLogin =await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(StudentForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExist(userForRegisterDto.TcIdentity);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(long tc, string OldPassword, string NewPassword)
        {
            var result =await _authService.ChangePassword(tc, OldPassword, NewPassword);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
