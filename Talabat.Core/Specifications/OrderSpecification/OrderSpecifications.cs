using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecification
{
	public class OrderSpecifications : BaseSpecifications<Order>
	{
        public OrderSpecifications(string buyerEmail) : base(o=>o.BuyerEmail== buyerEmail)
        {
            AddOrderIncludes();
			OrderByDesc = o => o.OrderDate;
        }

        public OrderSpecifications(string buyerEmail , int id) :
            base(o=>o.BuyerEmail== buyerEmail&&o.Id==id)
        {
			AddOrderIncludes();
		}

        private void AddOrderIncludes()
        {
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);
		}
    }
}
