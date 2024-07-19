using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Managers.Interfaces;

public interface IBrandManager
{
    public Task<Brand> GetOrCreate(string name, string model);
}