using AutoMapper;
using Talabat.APIS.DTO;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIS.Helpers
{
	public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)

			=> $"{_configuration["APIBaseUrl"]}/{source.Product.ProductUrl}";
	}
}
