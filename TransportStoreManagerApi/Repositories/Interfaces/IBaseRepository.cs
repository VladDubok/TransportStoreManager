using System.Linq.Expressions;

namespace TransportStoreManagerApi.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null);
    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task UpdateRangeAsync(IEnumerable<T> models);
    Task<T?> GetByIdAsync(long id);
}