﻿using System.Net;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;

namespace ECommerceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileService _fileService;


    public ProductsController(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository,
        ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository,
        IWebHostEnvironment webHostEnvironment, IFileService fileService)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();

        return Ok(new
        {
            totalCount,
            products
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _productReadRepository.GetByIdAsync(id, false));
    }

    [HttpPost]
    public async Task<IActionResult> Post(VM_Create_Product model)
    {
        if (ModelState.IsValid)
        {
        }

        await _productWriteRepository.AddAsync(new Product()
        {
            Name = model.Name,
            Price = model.Price,
            Stock = model.Stock
        });
        await _productWriteRepository.SaveAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put(VM_Update_Product model)
    {
        Product product = await _productReadRepository.GetByIdAsync(model.Id);
        product.Name = model.Name;
        product.Stock = model.Stock;
        product.Price = model.Price;
        await _productWriteRepository.SaveAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productWriteRepository.RemoveAsync(id);
        await _productWriteRepository.SaveAsync();
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        _fileService.UplaodAsync("resource/product-images", file);
        return Ok();
    }
}