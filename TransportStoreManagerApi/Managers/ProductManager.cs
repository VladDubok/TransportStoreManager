using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductManager(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task CreateProductAsync(AddProductRequestModel model)
    {
        var product = _mapper.Map<Product>(model);
        await _productRepository.AddAsync(product);
    }

    public async Task<IEnumerable<GetCustomerProductResponseModel>> GetCustomerProducts(long customerId)
    {
        var customerProducts = await _productRepository.GetCustomerProducts(customerId);
        
        return _mapper.Map<IEnumerable<GetCustomerProductResponseModel>>(customerProducts);
    }
}