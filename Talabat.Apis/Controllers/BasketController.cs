using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Apis.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasket(string id) 
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            if(basket == null)
                return Ok(new Basket(id));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> CreateOrUpdateBasket(BasketDto basket)
        {
            var mappedBasket = _mapper.Map<BasketDto, Basket>(basket);

            var createOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(mappedBasket);

            if (createOrUpdateBasket == null)
                return BadRequest(new ApiResponse(400));

            return Ok(basket);
        }

        [HttpDelete]
        public async Task deleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
 