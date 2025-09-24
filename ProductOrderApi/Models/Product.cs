using System.ComponentModel.DataAnnotations;

namespace ProductOrderApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = "";

        [StringLength(1000)]
        public string Description { get; set; } = "";

        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = "";
    }
}
