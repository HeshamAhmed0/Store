using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services_Absractions;
using Shared;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager,IOptions<JwtOptions> options) : IAuthService
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
                Tooken = await GenerateJwtTooken(user)

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
                Tooken =await GenerateJwtTooken(user)
            };

        }

        public async Task<string> GenerateJwtTooken(AppUser user)
        {
            var jwtParameters = options.Value;
            var Claimss = new List<Claim>()
            {
             new Claim(ClaimTypes.Email,user.Email),
             new Claim(ClaimTypes.Name,user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtParameters.SecurityKey));

            var tooken = new JwtSecurityToken(
               issuer: jwtParameters.Issuer,
               audience: jwtParameters.Audiences,
               claims: Claimss,
               expires: DateTime.UtcNow.AddDays(jwtParameters.DirationInDay),
               signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(tooken);
           
        }

    }
}
