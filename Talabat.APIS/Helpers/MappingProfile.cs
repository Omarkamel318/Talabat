using AutoMapper;
using Talabat.APIS.DTO;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Identities;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Products;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIS.Helpers
{
    public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d=>d.Category,o=>o.MapFrom(s=>s.Category.Name))
                //.ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=>$"URL{s.PictureUrl}")) //url changes by environment
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<ShippingAddressDto, ShippingAddress>();

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d=>d.ProductName,o=>o.MapFrom(s=>s.Product.ProductName))
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
				.ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.ProductUrl))
                .ForMember(d=>d.PictureUrl , o => o.MapFrom<OrderPictureUrlResolver>());


            CreateMap<Order,OrderToReturnDto>()
                .ForMember(d=>d.Status,o=>o.MapFrom(s=>s.Status))
                .ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
        }
    }
}
