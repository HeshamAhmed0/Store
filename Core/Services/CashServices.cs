using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.IdentityModel.Tokens;
using Services_Absractions;

namespace Services
{
    public class CashServices(ICachReposatory cachReposatory) : ICachService
    {
        public async Task<string?> GetAsync(string key)
        {
           var result=await cachReposatory.GetAsync(key);
            return result.IsNullOrEmpty() ? null : result;
        }

        public async Task SetAsync(string key, object value, TimeSpan timeSpan)
        {
           await cachReposatory.SetAsync(key, value, timeSpan);
        }
    }
}
