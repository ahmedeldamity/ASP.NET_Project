using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specifications
{
    public class ProductWithFilterationSpecificationsForCount: BaseSpecifications<Product>
    {
        public ProductWithFilterationSpecificationsForCount(ProductSpecificationParams specParams)
        {
            WhereCriteria = P =>
            (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
            (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId) &&
            (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower()));
        } 
    }
}