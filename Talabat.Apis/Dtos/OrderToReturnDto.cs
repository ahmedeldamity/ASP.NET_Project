using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Apis.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; } 

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderData { get; set; } 

        public string Status { get; set; }

        public Address ShipingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal Subtotal { get; set; } 

        public decimal Total { get; set; } 

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
