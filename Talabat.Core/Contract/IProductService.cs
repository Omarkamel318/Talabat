using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Products;
using Talabat.Core.Specifications;

namespace Talabat.Core.Contract
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetProductsAsync(ISpecifications<Product> spec);

		Task<Product?> GetProductAsync(ISpecifications<Product> spec);

		Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
		Task<IReadOnlyList<ProductCategory>> GetCategoryAsync();
		Task<int> GetCountAsync(ISpecifications<Product> spec);
	}
}
