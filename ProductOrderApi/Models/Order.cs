using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductOrderApi.Models
{
    public class Order
{
    public int OrderId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}

}
