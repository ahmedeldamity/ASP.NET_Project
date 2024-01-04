using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Apis.Dtos
{
    public class OrderDto
    {
        public string BuyerEmail { get; set; }

        public string BasketId { get; set;}

        [Required]
        public int DeliveryMethodId { get; set;}

        [Required]
        public Address ShippingAddress { get; set;}
    }
}
