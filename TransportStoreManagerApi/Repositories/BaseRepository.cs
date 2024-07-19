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

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsQueryable();
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

    public async Task<List<T>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}