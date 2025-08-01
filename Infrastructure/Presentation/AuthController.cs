using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;
using Shared;
using Shared.OrderDtos;

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
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result =await serviceManager.AuthService.CheckEmailExistsAsync(email);
            return Ok(result);
        }
        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var Email =User.FindFirstValue(ClaimTypes.Email);
            var result =await serviceManager.AuthService.GetCurrentUserAsync(Email);
            return Ok(result);
        }
        [HttpGet("UserAddress")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.GetCurrentUserAddressAsync(Email);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(OrderAdressDto orderAdressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.UpdateCurrentUserAddressAsync(orderAdressDto, Email);
            return Ok(result);
        }

    }
}
