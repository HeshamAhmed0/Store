using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Reposatory
{
    public class CachReposatory(IConnectionMultiplexer connection) : ICachReposatory
    {
        private readonly IDatabase _database =connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var result=await _database.StringGetAsync(key);
            return !result.IsNullOrEmpty ? result :default ;
        }

        public async Task SetAsync(string key, object value, TimeSpan TimeToleft)
        {
            var RedisValue=JsonSerializer.Serialize(value);
             await _database.StringSetAsync(key, RedisValue, TimeToleft);
        }
    }
}
