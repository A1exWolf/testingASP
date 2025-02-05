using Microsoft.AspNetCore.Mvc;
using TestingBackend.Data;
using TestingBackend.Models;

namespace TestingBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok($"Продукт {product.Name} добавлен успешно.");
    }
}