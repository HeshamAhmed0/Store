using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services_Absractions
{
    public interface IBasketService
    {
       Task<CustomerBasketDto?> GetBasketAsync(string id);
       Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto customerBasketDto);
       Task<bool> DeleteBasketAsync(string id);
    }
}
