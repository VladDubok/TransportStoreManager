using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Repositories;

public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
{
    public PromotionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Promotion?> GetByIdWithProductsAsync(long id)
    {
        return await GetAll()
            .Include(x => x.ProductPromotions)
                .ThenInclude(x => x.ProductPromotionHistories)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}