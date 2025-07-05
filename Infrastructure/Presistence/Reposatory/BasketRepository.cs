using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;

namespace Persistence.Reposatory
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database =connection.GetDatabase();
        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var radis =await _database.StringGetAsync(id);
            if (radis.IsNullOrEmpty) return null;

            var basket =JsonSerializer.Deserialize<CustomerBasket>(radis);
            if (basket == null) return null;
            return basket;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLife)
        {
            var basketAsjson =JsonSerializer.Serialize(basket);
            var flag =await _database.StringSetAsync(basket.Id, basketAsjson, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(basket.Id) : null;

        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await  _database.KeyDeleteAsync(id);      
        }
    }
}
