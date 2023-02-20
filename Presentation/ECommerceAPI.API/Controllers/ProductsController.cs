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

    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    private readonly ICustomerWriteRepository _customerWriteRepository;

    public ProductsController(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository,
        ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _orderWriteRepository = orderWriteRepository;
        _customerWriteRepository = customerWriteRepository;
        _orderReadRepository = orderReadRepository;
    }

    [HttpGet]
    public async Task Get()
    {
        var order = await _orderReadRepository.GetByIdAsync("0783EB3C-1B89-4BD2-7D7B-08DB136C87E3");
        order.Adress = "İstanbul";

        await _orderWriteRepository.SaveAsync();
    }
}