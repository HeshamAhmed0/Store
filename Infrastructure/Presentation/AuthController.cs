using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result=await serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result =await serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
