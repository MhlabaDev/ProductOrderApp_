using Microsoft.AspNetCore.Mvc;
using ProductOrderApi.Models;
using ProductOrderApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOrderApi.Controllers
{
[ApiController]
[Route("api/[controller]")]

public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
}

}
