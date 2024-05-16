namespace Talabat.Core.Entities.Order_Aggregate
{
	public class DeliveryMethod : EntityBase
	{
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }
    }
}