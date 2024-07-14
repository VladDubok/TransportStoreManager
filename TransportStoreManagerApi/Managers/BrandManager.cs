using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class BrandManager : IBrandManager
{
    private readonly IBaseRepository<Brand> _brandRepository;

    public BrandManager(IBaseRepository<Brand> brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<long> GetOrCreate(string name, string model)
    {
        var brands = await _brandRepository.GetAllAsync(x => x.Name == name && x.Model == model);
        var brandId = brands.FirstOrDefault()?.Id;
                
        if (brandId is null)
        {
            var newBrand = new Brand
            {
                Name = name,
                Model = model,
            };
            await _brandRepository.AddAsync(newBrand);
            brandId = newBrand.Id;
        }

        return brandId.Value;
    }
}