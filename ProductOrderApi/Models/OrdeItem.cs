using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductOrderApi.Models
{
    

public class OrderItem
{
    public int OrderItemId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [JsonIgnore] 
    public Order? Order { get; set; }

    public Product? Product { get; set; }
}

}
