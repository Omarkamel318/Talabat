using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Products;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductCountSpecifications : BaseSpecifications<Product> 
	{
        public ProductCountSpecifications(ProductSpecification spec)
			: base(p => (string.IsNullOrEmpty(spec.Name) || p.Name.ToLower().Contains(spec.Name))
						&&
						(!spec.BrandId.HasValue || p.BrandId == spec.BrandId)
						&&
						(!spec.CategoryId.HasValue || p.CategoryId == spec.CategoryId))
		{
            
        }

    }
}
