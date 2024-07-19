using Microsoft.AspNetCore.Mvc;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PromotionsController : ControllerBase
{
    private readonly IPromotionManager _promotionManager;

    public PromotionsController(IPromotionManager promotionManager)
    {
        _promotionManager = promotionManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var promotions = await _promotionManager.GetAllAsync();
        
        return Ok(promotions);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionRequest request)
    {
        await _promotionManager.CreatePromotionAsync(request);
        
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotionRequest request)
    {
        await _promotionManager.UpdatePromotionAsync(request);
        
        return Ok();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignPromotionToProduct(AssignPromotionToProductRequest request)
    {
        await _promotionManager.AssignToProductsAsync(request);
        
        return Ok();
    }

    [HttpPost("activate")]
    public async Task<IActionResult> ActivatePromotionToProduct(ActivatePromotionProductRequest request)
    {
        await _promotionManager.ActivatePromotionProductAsync(request);
        
        return Ok();
    }
}