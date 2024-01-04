using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _storeDbContext)
        {
            if (_storeDbContext.ProductBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _storeDbContext.ProductBrands.Add(brand);
                    }
                }
            }

            if (_storeDbContext.ProductCategories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        _storeDbContext.ProductCategories.Add(category);
                    }
                }
            }

            if (_storeDbContext.Products.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _storeDbContext.Products.Add(product);
                    }
                }
            }

            if (_storeDbContext.DeliveryMethods.Count() == 0)
            {
                var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count() > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        _storeDbContext.DeliveryMethods.Add(deliveryMethod);
                    }
                }
            }

            await _storeDbContext.SaveChangesAsync();
        }
    }
}
