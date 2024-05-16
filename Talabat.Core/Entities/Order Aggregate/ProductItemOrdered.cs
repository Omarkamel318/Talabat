namespace Talabat.Core.Entities.Order_Aggregate
{
	public class ProductItemOrdered
	{
        public ProductItemOrdered() // for EF
        {
            
        }

		public ProductItemOrdered(int productId, string productName, string productUrl)
		{
			ProductId = productId;
			ProductName = productName;
			ProductUrl = productUrl;
		}

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductUrl { get; set; }
	}
}