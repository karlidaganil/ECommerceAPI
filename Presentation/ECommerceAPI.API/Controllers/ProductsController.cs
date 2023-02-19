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
    public async Task GetProducts()
    {
        Product p = await _productReadRepository.GetByIdAsync("043FFD31-70B5-45F1-A33A-1623B84C6330");
        p.Name = "Ha şöyle";
        await _productWriteRepository.SaveAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        // var test = await _productReadRepository.GetByIdAsync(id);
        // return Ok(test);
        return Ok();
    }
}