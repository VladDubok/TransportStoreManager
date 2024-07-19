using Microsoft.EntityFrameworkCore;
using Serilog;
using TransportStoreManagerApi.Adapters;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class PriceManager : IPriceManager
{
    private const string UsdCode = "USD";
    private readonly IBaseRepository<ProductPrice> _priceRepository;
    private readonly IFrankfurterCurrencyExchangeAdapter _exchangeAdapter;

    public PriceManager(
        IBaseRepository<ProductPrice> priceRepository,
        IFrankfurterCurrencyExchangeAdapter exchangeAdapter)
    {
        _priceRepository = priceRepository;
        _exchangeAdapter = exchangeAdapter;
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

        if (code != UsdCode)
        {
            var usdRate = await _exchangeAdapter.GetUsdValue(code, price);
            prices.Add(new ProductPrice
            {
                ProductId = productId,
                CurrencyCode = UsdCode,
                Price = usdRate
            });
        }

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
        
        var productPrices = await _priceRepository.GetAll().Where(x => x.ProductId == productId).ToListAsync();
        var currencyPrice = productPrices.First(x => x.CurrencyCode == code);
        currencyPrice.Price = price;
        
        if (code != UsdCode)
        {
            var usdRate = await _exchangeAdapter.GetUsdValue(code, price);
            var usdPrice = productPrices.FirstOrDefault(x => x.CurrencyCode == UsdCode);
            
            if (usdPrice is null)
            {
                var newProductPrice = new ProductPrice
                {
                    ProductId = productId,
                    CurrencyCode = UsdCode,
                    Price = usdRate
                };

                await _priceRepository.AddAsync(newProductPrice);
            }
            else
            {
                usdPrice.Price = usdRate;
            }
        }

        await _priceRepository.UpdateRangeAsync(productPrices);
    }

    private bool IsValidCurrencyCode(string code)
    {
        return ISO._4217.CurrencyCodesResolver.Codes.Any(x => x.Code == code);
    }
}