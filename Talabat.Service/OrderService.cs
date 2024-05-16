using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Products;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications.OrderSpecification;
using Talabat.Infrastructure;
using Talabat.Repository;

namespace Talabat.Application
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;
		

		public OrderService(
			IBasketRepository basketRepo,
			IUnitOfWork unitOfWork)
        {
			_basketRepo = basketRepo;
			_unitOfWork = unitOfWork;
		}
        public async Task<Order?> CreateOrderAsync(string basketId, string buyerEmail, ShippingAddress shippingAddress, int deliveryMethodId)
		{
			var orderItems= new List<OrderItem>();
			var basket = await _basketRepo.GetBasketAsync(basketId);
			if(basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);

					var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

					var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

					orderItems.Add(orderItem);
				}
			}

			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
			var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

			var order = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = shippingAddress,
				DeliveryMethod = deliveryMethod,
				Items = orderItems,
				SubTotal = subTotal
			};
			_unitOfWork.Repository<Order>().Add(order);
			var result = await _unitOfWork.CompleteAsync();

			if (result <= 0)
				return null;

			return order;
		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		=>_unitOfWork.Repository<DeliveryMethod>().GetAllAsync();


		public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
		{
			var orderSpec = new OrderSpecifications(buyerEmail, orderId);
			return await _unitOfWork.Repository<Order>().GetWithSpecAsync(orderSpec);
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var orderSpec = new OrderSpecifications(buyerEmail);
			return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(orderSpec);
		}
	}
}
