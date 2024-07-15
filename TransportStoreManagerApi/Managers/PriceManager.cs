using Serilog;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class PriceManager : IPriceManager
{
    private readonly IBaseRepository<ProductPrice> _priceRepository;

    public PriceManager(IBaseRepository<ProductPrice> priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task CreatePricesAsync(string code, decimal price, long productId)
    {
        if (!IsValidCurrencyCode(code))
        {
            Log.Error("Currency code {0}, is not valid");
            // TODO: Create InvalidCurrencyCodeException
            throw new Exception("InvalidCurrencyCode");
        }

        var prices = new List<ProductPrice>
        {
            new ProductPrice
            {
                Price = price,
                CurrencyCode = code,
                ProductId = productId
            }
        };

        await _priceRepository.AddRangeAsync(prices);
    }

    public async Task UpdateProductPriceAsync(long productId, decimal price, string code)
    {
        if (!IsValidCurrencyCode(code))
        {
            Log.Error("Currency code {0}, is not valid");
            // TODO: Create InvalidCurrencyCodeException
            throw new Exception("InvalidCurrencyCode");
        }
        
        var productPrices = await _priceRepository.GetAllAsync(x => x.ProductId == productId);
        var currencyPrice = productPrices.First(x => x.CurrencyCode == code);
        currencyPrice.Price = price;

        await _priceRepository.UpdateAsync(currencyPrice);
    }

    private bool IsValidCurrencyCode(string code)
    {
        return ISO._4217.CurrencyCodesResolver.Codes.Any(x => x.Code == code);
    }
}