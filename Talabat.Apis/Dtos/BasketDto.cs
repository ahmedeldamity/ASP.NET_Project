using Talabat.Core.Entities;

namespace Talabat.Apis.Dtos
{
    public class BasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
