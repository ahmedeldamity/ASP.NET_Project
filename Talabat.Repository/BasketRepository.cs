using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redisDbContext;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _redisDbContext = redis.GetDatabase();
        }

        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket)
        {
            var createdOrUpdated = _redisDbContext.StringSet(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            
            if(createdOrUpdated == false) 
                return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redisDbContext.KeyDeleteAsync(basketId);
        }

        public async Task<Basket?> GetBasketAsync(string basketId)
        {
            var basket = await _redisDbContext.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
        }
    }
}
