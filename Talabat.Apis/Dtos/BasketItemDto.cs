using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must not be zero!")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must not be zero!")]
        public int Quantity { get; set; }
    }
}
