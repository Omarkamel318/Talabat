using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Products;
using Talabat.Core.Specifications;

namespace Talabat.Application
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
			=> await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

		public async Task<IReadOnlyList<ProductCategory>> GetCategoryAsync()
			=> await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

		public async Task<int> GetCountAsync(ISpecifications<Product> spec)
			=> await _unitOfWork.Repository<Product>().CountAsync(spec);

		public async Task<Product?> GetProductAsync(ISpecifications<Product> spec)
			=> await _unitOfWork.Repository<Product>().GetWithSpecAsync(spec);

		public async Task<IReadOnlyList<Product>> GetProductsAsync(ISpecifications<Product> spec)
			=> await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
	}
}
