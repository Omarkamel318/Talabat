namespace Talabat.Core.Entities.Order_Aggregate
{
	public class OrderItem : EntityBase
	{
        private OrderItem() // for EF
        {
            
        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
		{
			Product = product;
			Price = price;
			Quantity = quantity;
		}

		public ProductItemOrdered Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}