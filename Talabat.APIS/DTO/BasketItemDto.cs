using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTO
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		[Range(0.9,double.MaxValue ,ErrorMessage ="Price must be at least 1")]
		public decimal Price { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Category { get; set; }
		[Range(1,int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
		[Required]
		public int Quantity { get; set; }

	}
}