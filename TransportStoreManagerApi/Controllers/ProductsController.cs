using Microsoft.AspNetCore.Mvc;
using TransportStoreManagerApi.Managers;
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
        var customerProducts = await _productManager.GetCustomerProducts(id);
        
        return Ok(customerProducts);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequestModel request)
    {
        await _productManager.CreateProductAsync(request);
        return Ok();
    }
}