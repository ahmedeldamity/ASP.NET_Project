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
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositories<Product> productsRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var specifications = new ProductWithBrandAndCategorySpecifications();

            var products = await _productsRepo.GetAllWithSpecificationsAsync(specifications);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
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
    }
}