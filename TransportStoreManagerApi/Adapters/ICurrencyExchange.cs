namespace TransportStoreManagerApi.Adapters;

public interface ICurrencyExchange
{
    Task<decimal> GetUsdValue(string code, decimal value);
}