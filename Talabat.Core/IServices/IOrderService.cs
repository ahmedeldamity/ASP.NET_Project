using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
