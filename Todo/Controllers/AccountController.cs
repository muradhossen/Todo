using Application.DTOs.Users;
using Application.ServiceInterfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Todo.Controllers.Base;

namespace Todo.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegistrationDTO registerDto)
        //{ 
        //     var result =await _userService.Registration(registerDto);

        //    if (result.IsSuccess) return Ok(result);

        //    return BadRequest(result);
        //}

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            var result = await _userService.Login(loginDto);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
    }
}
