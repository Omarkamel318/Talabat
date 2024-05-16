using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Products;
using Talabat.Repository.Data;

namespace Talabat.Infrastructure.Data.DataSeeding
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.ProductBrands.Count() == 0)
            {
                var brandsText = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsText);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbcontext.ProductBrands.Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.ProductCategories.Count() == 0)
            {
                var categoriesText = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesText);

                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbcontext.ProductCategories.Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.Products.Count() == 0)
            {
                var productsText = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsText);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        _dbcontext.Products.Add(product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

			if (_dbcontext.DeliveryMethods.Count() == 0)
			{
				var deliveryMethodsText = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
				var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsText);

				if (deliveryMethods?.Count > 0)
				{
					foreach (var DM in deliveryMethods)
					{
						_dbcontext.DeliveryMethods.Add(DM);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}


		}
    }
}
