using System.Linq.Expressions;

namespace TransportStoreManagerApi.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll();
    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task UpdateRangeAsync(IEnumerable<T> models);
    Task<T?> GetByIdAsync(long id);
    Task<List<T>> GetByIdsAsync(IEnumerable<long> ids);
}