using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specifications
{
    public class ProductWithBrandAndCategorySpecifications: BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId)
        {
            WhereCriteria = P => 
            (!brandId.HasValue || P.BrandId == brandId) && 
            (!categoryId.HasValue || P.CategoryId == categoryId);

            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category); 

            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        OrderBy = p => p.Price;
                        break;
                    case "priceDesc":
                        OrderByDesc = p => p.Price;
                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;
                }
            }
            else
            {
                OrderBy = p => p.Name;
            }
        }
        public ProductWithBrandAndCategorySpecifications(int id)
        {
            WhereCriteria = p => p.Id == id;
            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category);
        }
    }
}
