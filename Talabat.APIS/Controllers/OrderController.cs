using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.APIS.DTO;
using Talabat.APIS.Error;
using Talabat.Application;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIS.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class OrderController : BaseAPIController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(
			IOrderService orderService ,
			IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}

        [HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var address = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(orderDto.BasketId, orderDto.BuyerEmail, address, orderDto.DeliveryMethodId);
			if (order is null)
				return BadRequest(new APIResponse(400));

			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>?>> GetOrders(string email)
		{
			var orders =  await _orderService.GetOrdersForUserAsync(email);

			if (orders.Count <= 0)
				return NotFound(new APIResponse(404));

			return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id , string email /*from token*/)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(id, email);

			if (order is null)
				return NotFound(new APIResponse(404));

			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}

		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

			return Ok(deliveryMethods);
		}
	}
}
