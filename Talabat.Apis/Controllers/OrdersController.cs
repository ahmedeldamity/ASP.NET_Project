using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities.Order_Aggregiate;
using Talabat.Core.IServices;

namespace Talabat.Apis.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, orderDto.ShippingAddress);

            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrdersForUser(string buyerEmail)
        {
            var orders = await _orderService.GetAllOrdersForUserAsync(buyerEmail);

            return Ok(orders);
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id, string buyerEmail)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);

            if(order is null)
                return NotFound(new ApiResponse(404));

            return Ok(order);
        }
    }
}
