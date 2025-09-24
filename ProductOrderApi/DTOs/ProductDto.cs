namespace ProductOrderApi.DTOs
{
    /// <summary>
    /// DTO for transferring product data
    /// Keeps only exposed fields for clients
    /// </summary>
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
    }
}
