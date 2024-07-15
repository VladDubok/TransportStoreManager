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

    public async Task<Brand> GetOrCreate(string name, string model)
    {
        var brands = await _brandRepository.GetAllAsync(x => x.Name == name && x.Model == model);

        if (brands.FirstOrDefault() is not null)
        {
            return brands.First();
        }

        var newBrand = new Brand
        {
            Name = name,
            Model = model,
        };
        await _brandRepository.AddAsync(newBrand);

        return newBrand;
    }
}