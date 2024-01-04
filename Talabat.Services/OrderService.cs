using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregiate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.Order_Specifications;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get basket from basket repository
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // 2. Get Items at Basket from Product repository
            var orderitems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderitems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal
            var subTotal = orderitems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get Delivery Method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderitems, subTotal);

            await _unitOfWork.Repository<Order>().AddAsync(order);

            // 6. SaveChanges()
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepo = _unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(buyerEmail);

            var orders = await ordersRepo.GetAllWithSpecificationsAsync(spec);

            return orders;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var ordersRepo = _unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(orderId, buyerEmail);

            var order = await ordersRepo.GetByIdWithSpecificationsAsync(spec);

            if(order is null) return null;

            return order;
        }
    }
}
