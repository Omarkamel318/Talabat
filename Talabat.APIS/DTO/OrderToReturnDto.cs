using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIS.DTO
{
	public class OrderToReturnDto
	{
		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
		public string Status { get; set; }
		public ShippingAddress ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
		public string DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

		public decimal SubTotal { get; set; } // without delivery
		public decimal Total { get; set; }

	}
}
