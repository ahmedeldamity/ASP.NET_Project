using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
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
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var specifications = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productsRepo.GetByIdWithSpecificationsAsync(specifications);

            if (product is null)
                return NotFound();

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
    }
}
