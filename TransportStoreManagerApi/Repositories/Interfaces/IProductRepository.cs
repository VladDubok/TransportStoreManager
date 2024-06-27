using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Repositories.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetCustomerProducts(long customerId);
}