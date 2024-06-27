using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetCustomerProducts(long customerId)
    {
        var customerProducts = await _context.Products
            .AsNoTracking()
            .Include(x => x.Brand)
            .Include(x => x.Currency)
            .Include(x => x.ProductType)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        return customerProducts;
    }
}