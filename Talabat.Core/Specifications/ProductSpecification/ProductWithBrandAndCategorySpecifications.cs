using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Products;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product> 
	{
        public ProductWithBrandAndCategorySpecifications()
        {
            
        }


        public ProductWithBrandAndCategorySpecifications(ProductSpecification spec) 
			:base(p =>	( string.IsNullOrEmpty(spec.Name)|| p.Name.ToLower().Contains(spec.Name) )
						&&
						(!spec.BrandId.HasValue || p.BrandId == spec.BrandId)
						&&
						(!spec.CategoryId.HasValue || p.CategoryId == spec.CategoryId)) // for get all
        {
			AddIncludes();
			//pageSize = 5
			//pageIndex = 3 
			AddPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
		


			if (!string.IsNullOrEmpty(spec.Sort))
			{
				switch (spec.Sort.ToLower())
				{
					case "priceasc":
						OrderBy = p => p.Price;
						break;
					case "pricedesc":
						OrderByDesc = p => p.Price;
						break;
					default:
						OrderBy = p => p.Name;
						break;
				}
			}
			else
				OrderBy = p => p.Name;

		}

		public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id) // for get by id
		{
			AddIncludes();
		}
		private void AddIncludes()
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}
