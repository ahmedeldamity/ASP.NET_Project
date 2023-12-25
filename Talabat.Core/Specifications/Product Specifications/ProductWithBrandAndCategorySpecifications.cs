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
        public ProductWithBrandAndCategorySpecifications(): base()
        {
            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            IncludeCriterias.Add(c => c.Brand);
            IncludeCriterias.Add(c => c.Category);
        }
    }
}
