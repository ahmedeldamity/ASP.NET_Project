using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specifications
{
    public class ProductWithAllSpecifications: BaseSpecifications<Product>
    {
        public ProductWithAllSpecifications(ProductSpecificationParams specParams)
        {
            WhereCriteria = P => 
            (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) && 
            (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId) &&
            (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower()));

            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category); 

            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
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

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
        public ProductWithAllSpecifications(int id)
        {
            WhereCriteria = p => p.Id == id;
            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category);
        }
    }
}
