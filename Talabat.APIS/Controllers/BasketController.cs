using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Talabat.APIS.DTO;
using Talabat.APIS.Error;
using Talabat.Core.Entities.Basket;
using Talabat.Core.IRepositories;

namespace Talabat.APIS.Controllers
{

    public class BasketController : BaseAPIController
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(
			IBasketRepository basketRepo,
			IMapper mapper
			)
        {
			_basketRepo = basketRepo;
			_mapper = mapper;
		}
        [HttpGet]
		public async Task<ActionResult<Task<CustomerBasket?>>>GetBasket(string id)
		{
			var basket = await _basketRepo.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<Task<CustomerBasket?>>>UpdateBasket(CustomerBasketDto basket)
		{
			var customerBasket =_mapper.Map<CustomerBasketDto, CustomerBasket>(basket); // added in maping profile
			var newBasket = await _basketRepo.UpdateBasketAsync(customerBasket);
			if (newBasket is null)
			{
				return BadRequest(new APIResponse(400));

			}
			return Ok(newBasket);
		}

		[HttpDelete]

		public async Task<ActionResult<APIResponse>> DeleteBasket(string id)
		{
			var isDeleted = await _basketRepo.DeleteBasketAsync(id);
			return isDeleted ? Ok(new APIResponse(200, "Basket is deleted")) : BadRequest(new APIResponse(400, "Basket is not Exist"));
		}
	}
}		
