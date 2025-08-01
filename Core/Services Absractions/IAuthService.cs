using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.OrderDtos;

namespace Services_Absractions
{
    public interface IAuthService
    {
        Task<LoginResultDto> LoginAsync(LoginDto loginDto);

        Task<LoginResultDto> RegisterAsync(RegisterDto registerDto);
        
        Task <bool> CheckEmailExistsAsync(string email);
        Task<LoginResultDto> GetCurrentUserAsync(string email);
        Task<OrderAdressDto> GetCurrentUserAddressAsync(string email);
        Task<OrderAdressDto> UpdateCurrentUserAddressAsync(OrderAdressDto orderAdressDto ,string email);

    }
}
