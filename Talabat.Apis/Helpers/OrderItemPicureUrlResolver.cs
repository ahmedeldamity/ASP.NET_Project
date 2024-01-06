using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Apis.Helpers
{
    public class OrderItemPicureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPicureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";

            return string.Empty;
        }
    }
}
