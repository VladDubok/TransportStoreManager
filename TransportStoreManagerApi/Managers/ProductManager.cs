using System.Text.Json;
using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Messages;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IBaseRepository<BlobFile> _fileRepository;
    private readonly IBaseRepository<OutboxMessage> _outboxRepository;
    private readonly IMapper _mapper;

    public ProductManager(
        IProductRepository productRepository,
        IMapper mapper,
        IBaseRepository<BlobFile> fileRepository,
        IBaseRepository<OutboxMessage> outboxRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _fileRepository = fileRepository;
        _outboxRepository = outboxRepository;
    }

    public async Task CreateProductAsync(AddProductRequestModel model)
    {
        var product = _mapper.Map<Product>(model);
        await _productRepository.AddAsync(product);
    }

    public async Task<IEnumerable<GetCustomerProductResponseModel>> GetCustomerProductsAsync(long customerId)
    {
        var customerProducts = await _productRepository.GetCustomerProducts(customerId);

        return _mapper.Map<IEnumerable<GetCustomerProductResponseModel>>(customerProducts);
    }

    public async Task UpdateAsync(long id, UpdateProductRequestModel request)
    {
        var toUpdateProduct = _mapper.Map<Product>(request);
        toUpdateProduct.Id = id;

        await _productRepository.UpdateAsync(toUpdateProduct);
    }

    public async Task UploadProductFile(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var blobFile = new BlobFile
        {
            Data = stream.ToArray()
        };
        await _fileRepository.AddAsync(blobFile);

        var data = JsonSerializer.Serialize(new ProductFileUploadedMessage(blobFile.Id));
        var message = new OutboxMessage
        {
            Name = "product-file-uploaded",
            Data = data
        };
        await _outboxRepository.AddAsync(message);
    }
}