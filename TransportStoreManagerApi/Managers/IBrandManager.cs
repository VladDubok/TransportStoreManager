namespace TransportStoreManagerApi.Managers;

public interface IBrandManager
{
    public Task<long> GetOrCreate(string name, string model);
}