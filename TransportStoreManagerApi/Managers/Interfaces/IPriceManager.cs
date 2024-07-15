namespace TransportStoreManagerApi.Managers;

public interface IPriceManager
{
    public Task CreatePricesAsync(string code, decimal price, long productId);
    Task UpdateProductPriceAsync(long productId, decimal requestPrice, string requestCurrencyCode);
}