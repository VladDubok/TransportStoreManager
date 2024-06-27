using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data;

namespace TransportStoreManagerApi.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression)
    {
        var query = _context.Set<T>().AsQueryable();
        
        if (expression is not null)
        {
            query = query.Where(expression);
        }
        
        return await query.ToListAsync();
    }

    public async Task AddAsync(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> models)
    {
        await _context.Set<T>().AddRangeAsync(models);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T model)
    {
        _context.Set<T>().Update(model);
        await _context.SaveChangesAsync();
    }
}