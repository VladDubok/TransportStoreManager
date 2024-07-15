using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Managers;

public interface IBrandManager
{
    public Task<Brand> GetOrCreate(string name, string model);
}