using System;
using System.Collections.Generic;

namespace ProductOrderApi.DTOs
{
    /// <summary>
    /// DTO for transferring order data
    /// Includes customerId only to avoid cycles
    /// </summary>
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
