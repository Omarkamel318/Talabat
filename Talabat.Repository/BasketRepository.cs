using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.Basket;
using Talabat.Core.IRepositories;

namespace Talabat.Infrastructure
{
    public class BasketRepository : IBasketRepository
	{
		private IDatabase _database;
		public BasketRepository(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
        }

		public async Task<CustomerBasket?> GetBasketAsync(string id)
		{
			var basket = await _database.StringGetAsync(id); // returned as json

			return basket.IsNullOrEmpty ? null:JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(10));
			return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
		}

		public async Task<bool> DeleteBasketAsync(string id)
		{
			return await _database.KeyDeleteAsync(id);
		}

	}
}
