using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIS.DTO;
using Talabat.Core.Entities.Products;

namespace Talabat.APIS.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}

		public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			return $"{_configuration["APIBaseUrl"]}/{source.PictureUrl}";
		}
	}
}
