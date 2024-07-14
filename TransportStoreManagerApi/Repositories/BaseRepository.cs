using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TransportStoreManagerApi.Data;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
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

    public async Task UpdateRangeAsync(IEnumerable<T> models)
    {
        _context.Set<T>().UpdateRange(models);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            Log.Information(string.Format($"Entity {0} with id: {1} was not found", typeof(T).Name, id));
        }

        return result;
    }
}