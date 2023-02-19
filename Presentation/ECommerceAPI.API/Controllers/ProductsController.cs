using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public ProductsController(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        await _productWriteRepository.AddRangeAsync(new()
        {
            new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 10, CreatedDate = DateTime.Now, },
            new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 10, CreatedDate = DateTime.Now, },
            new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Stock = 10, CreatedDate = DateTime.Now, },
            new() { Id = Guid.NewGuid(), Name = "Product 4", Price = 400, Stock = 10, CreatedDate = DateTime.Now, }
        });
        await _productWriteRepository.SaveAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        var test = await _productReadRepository.GetByIdAsync(id);
        return Ok(test);
    }
}