namespace TransportStoreManagerApi.Adapters;

public class FrankfurterCurrencyExchangeAdapter : IFrankfurterCurrencyExchangeAdapter
{
    private const string UsdCode = "USD";
    private readonly HttpClient _httpClient;

    public FrankfurterCurrencyExchangeAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetUsdValue(string code, decimal value)
    {
        var response = await _httpClient.GetAsync("latest");
        var currencyRateModel = await response.Content.ReadFromJsonAsync<CurrencyRateModel>();
        decimal rate = 0;
        var isExistRate = currencyRateModel?.Rates.TryGetValue(code, out rate);

        if (isExistRate is null)
        {
            throw new Exception($"Currency {code} does not exist in FrankfurterCurrencyExchange");
        }

        decimal usdRate = 0;    
        currencyRateModel?.Rates.TryGetValue(UsdCode, out usdRate);


        var usdValue = value / rate * usdRate;

        return usdValue;
    }
}

public class CurrencyRateModel
{
    public decimal Amount { get; set; }
    public string Base { get; set; }
    public DateTime Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}