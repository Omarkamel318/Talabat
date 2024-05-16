using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIS.DTO
{
	public class OrderDto
	{
        [Required]
        public string BasketId { get; set; }
		[Required]
		public string BuyerEmail { get; set; }
		[Required]
        public ShippingAddressDto ShippingAddress { get; set; }
		[Required]
		public int DeliveryMethodId { get; set; }
    }
}
