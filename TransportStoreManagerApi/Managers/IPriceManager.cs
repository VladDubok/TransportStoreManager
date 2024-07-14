namespace TransportStoreManagerApi.Managers;

public interface IPriceManager
{
    public Task CreatePricesAsync(string code, decimal price, long productId);
}