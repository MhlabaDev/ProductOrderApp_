namespace ProductOrderApi.DTOs
{
    /// <summary>
    /// DTO for transferring order item data
    /// Prevents circular references with Order/Product
    /// </summary>
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Optional: include product details in lightweight form
        public ProductDto? Product { get; set; }
    }
}
