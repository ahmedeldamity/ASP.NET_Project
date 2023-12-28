using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications.Product_Specifications;

namespace Talabat.Apis.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepositories<Product> _productsRepo;
        private readonly IGenericRepositories<ProductBrand> _brandsRepo;
        private readonly IGenericRepositories<ProductCategory> _categoriesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositories<Product> productsRepo,
            IGenericRepositories<ProductBrand> brandsRepo,
            IGenericRepositories<ProductCategory> categoriesRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string? sort, int? brandId, int? categoryId)
        {
            var specifications = new ProductWithBrandAndCategorySpecifications(sort, brandId, categoryId);

            var products = await _productsRepo.GetAllWithSpecificationsAsync(specifications);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specifications = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productsRepo.GetByIdWithSpecificationsAsync(specifications);

            if (product is null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}