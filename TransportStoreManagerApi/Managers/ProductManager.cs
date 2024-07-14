using System.Text.Json;
using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Data.Entities.Enums;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Messages;
using TransportStoreManagerApi.Models.Dtos;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IBaseRepository<BlobFile> _fileRepository;
    private readonly IBaseRepository<OutboxMessage> _outboxRepository;
    private readonly IBaseRepository<Customer?> _customerRepository;
    private readonly IMapper _mapper;

    public ProductManager(
        IProductRepository productRepository,
        IMapper mapper,
        IBaseRepository<BlobFile> fileRepository,
        IBaseRepository<OutboxMessage> outboxRepository,
        IBaseRepository<Customer?> customerRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _fileRepository = fileRepository;
        _outboxRepository = outboxRepository;
        _customerRepository = customerRepository;
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

    public async Task UploadCustomerProductFile(IFormFile file, long customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            throw new Exception($"Customer with id: {customerId} was not found");
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var blobFile = new BlobFile
        {
            Data = stream.ToArray()
        };
        await _fileRepository.AddAsync(blobFile);

        var data = JsonSerializer.Serialize(new ProductFileUploadedMessage(blobFile.Id, customerId));
        var message = new OutboxMessage
        {
            Name = "product-file-uploaded",
            Data = data,
            Status = MessageStatusEnum.Created
        };
        await _outboxRepository.AddAsync(message);
    }

    public Task UpdateFromFile(IEnumerable<FileProductDto> products)
    {
        var toInsert = products.Where(x => x.Id is null).ToList();
        _productRepository.AddRangeAsync(toInsert);

        var toUpdate = products.Where(x => x.Id is not null).ToList();
    }
}