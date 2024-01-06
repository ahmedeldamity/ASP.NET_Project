using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specifications;

namespace Talabat.Core.IServices
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecificationParams specParams);
        Task<int> GetCountAsync(ProductSpecificationParams specParams);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync();
    }
}
