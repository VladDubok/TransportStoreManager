using Microsoft.EntityFrameworkCore;

namespace TransportStoreManagerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
}