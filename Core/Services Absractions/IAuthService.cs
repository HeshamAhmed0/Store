using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services_Absractions
{
    public interface IAuthService
    {
         Task<LoginResultDto> LoginAsync(LoginDto loginDto);
         
         Task<LoginResultDto> RegisterAsync(RegisterDto registerDto);

    }
}
