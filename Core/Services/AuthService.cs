using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services_Absractions;
using Shared;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
          var user =await userManager.FindByEmailAsync(loginDto.Email);
              if (user is null) throw new Exception();
                var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(result == false) throw new Exception();
            return new LoginResultDto()
            {
                DispalyName = user.DisplayName,
                Email = user.Email,
                Tooken = "Tooken"
            };
        }

        public async Task<LoginResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName =registerDto.UserName,
            };
            var result =await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception();
            }
            return new LoginResultDto()
            {
                Email = registerDto.Email,
                DispalyName = user.DisplayName,
                Tooken = "Tooken"
            };

        }
    }
}
