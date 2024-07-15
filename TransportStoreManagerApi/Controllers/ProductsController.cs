using Microsoft.AspNetCore.Mvc;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetCustomerProducts([FromRoute] long id)
    {
        var customerProducts = await _productManager.GetCustomerProductsAsync(id);
        
        return Ok(customerProducts);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequestModel request)
    {
        await _productManager.CreateProductAsync(request);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] long id,
        [FromBody] UpdateProductRequestModel request)
    {
        await _productManager.UpdateProductAsync(id, request);

        return Ok();
    }

    [HttpPost("customer/{customerId}/upload-file")]
    public async Task<IActionResult> UploadProductFile(
        [FromRoute] long customerId,
        IFormFile file)
    {
        await _productManager.UploadCustomerProductFile(file, customerId);
        
        return Ok();
    }
}