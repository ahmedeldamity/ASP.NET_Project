using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.Product_Specifications;

namespace Talabat.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecificationParams specParams)
        {
            var specifications = new ProductWithAllSpecifications(specParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecificationsAsync(specifications);

            return products;
        }
        public async Task<int> GetCountAsync(ProductSpecificationParams specParams)
        {
            var specificationsForCount = new ProductWithFilterationSpecificationsForCount(specParams);

            var count = await _unitOfWork.Repository<Product>().GetCountAsync(specificationsForCount);

            return count;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var specifications = new ProductWithAllSpecifications(productId);

            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecificationsAsync(specifications);

            return product;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return brands;
        }
        public async Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

            return categories;
        }
    }
}
