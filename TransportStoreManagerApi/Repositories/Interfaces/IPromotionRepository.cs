using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Repositories.Interfaces;

public interface IPromotionRepository : IBaseRepository<Promotion>
{
    public Task<Promotion?> GetByIdWithProductsAsync(long id);
}