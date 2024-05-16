using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Basket;

namespace Talabat.Core.IRepositories
{
    public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string id);

		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket); //add & update 

		Task<bool> DeleteBasketAsync(string id);
	}
}
