using TransportStoreManagerApi.Controllers;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Managers.Interfaces;

public interface IPromotionManager
{
    public Task CreatePromotionAsync(CreatePromotionRequest request);
    public Task UpdatePromotionAsync(UpdatePromotionRequest request);
    public Task AssignToProductsAsync(AssignPromotionToProductRequest request);
    Task<IEnumerable<Promotion>> GetAllAsync();
    Task ActivatePromotionProductAsync(ActivatePromotionProductRequest request);
}