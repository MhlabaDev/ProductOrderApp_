using Microsoft.AspNetCore.Mvc;
using ProductOrderApi.Models;
using ProductOrderApi.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
public async Task<IActionResult> CreateOrder([FromBody] Order order)
{
    if (order == null || order.Items.Count == 0)
        return BadRequest("Order is empty");

    // Only attach existing Customer
    var existingCustomer = await _context.Customers.FindAsync(order.CustomerId);
    if (existingCustomer == null)
        return BadRequest($"Customer with ID {order.CustomerId} not found");

    order.Customer = existingCustomer; // attach tracked entity

    foreach (var item in order.Items)
    {
        var product = await _context.Products.FindAsync(item.ProductId);
        if (product == null)
            return BadRequest($"Product with ID {item.ProductId} not found");

        item.UnitPrice = product.Price;
        item.Product = product; // attach tracked entity
    }

    order.CreatedAt = DateTime.UtcNow;

    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(CreateOrder), new { id = order.OrderId }, order);
}

}
