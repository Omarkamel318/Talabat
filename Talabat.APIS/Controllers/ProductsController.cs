using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Talabat.APIS.DTO;
using Talabat.APIS.Error;
using Talabat.APIS.Helpers;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Products;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.APIS.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : BaseAPIController
	{
		private readonly IMapper _mapper;
		private readonly IProductService _productService;

		public ProductsController(
			IMapper mapper ,
			IProductService productService
			)
        {
			_mapper = mapper;
			_productService = productService;
		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetAllProducts([FromQuery]ProductSpecification productSpec) //Send as query string
			{

			var spec = new ProductWithBrandAndCategorySpecifications(productSpec);

			var products = await _productService.GetProductsAsync(spec);

			var countSpec = new ProductCountSpecifications(productSpec);

			var count = await _productService.GetCountAsync(countSpec);

			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

			return Ok(new Pagination<ProductToReturnDTO>(productSpec.PageIndex, productSpec.PageSize, data , count));  //create obj result (Json) with status code 200

		}
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), 200)]
		[ProducesResponseType(typeof(APIResponse), 404)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			//var product = await _productRepo.GetAsync(id);
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productService.GetProductAsync(spec);
			if (product is null) 
				return NotFound(new APIResponse(404)); // handel error for specific form

			return Ok(_mapper.Map<Product,ProductToReturnDTO>(product));
		}
		[HttpGet("Brands")]  //baseUrl/api/products/categories
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var brands =await _productService.GetBrandsAsync();
			return Ok(brands);
		}
		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllCategories()
		{
			var categories = await _productService.GetCategoryAsync();
			return Ok(categories);
		}



	}
}
