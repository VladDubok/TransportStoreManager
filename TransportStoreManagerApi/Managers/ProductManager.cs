using System.Text.Json;
using AutoMapper;
using TransportStoreManagerApi.Data;
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
    private readonly IBaseRepository<Customer> _customerRepository;
    private readonly IBrandManager _brandManager;
    private readonly IPriceManager _priceManager;
    private readonly IBaseRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public ProductManager(
        IProductRepository productRepository,
        IMapper mapper,
        IBaseRepository<BlobFile> fileRepository,
        IBaseRepository<OutboxMessage> outboxRepository,
        IBaseRepository<Customer> customerRepository,
        IBaseRepository<ProductType> productTypeRepository,
        IBrandManager brandManager, IPriceManager priceManager, AppDbContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _fileRepository = fileRepository;
        _outboxRepository = outboxRepository;
        _customerRepository = customerRepository;
        _productTypeRepository = productTypeRepository;
        _brandManager = brandManager;
        _priceManager = priceManager;
        _context = context;
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

    public async Task UpdateFromFile(IEnumerable<UploadFileProductDto> products)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            foreach (var productDto in products)
            {
                var productTypeId = await _productTypeRepository.GetByIdAsync(productDto.ProductTypeId);
                
                var brandId = await _brandManager.GetOrCreate(productDto.BrandName, productDto.BrandModel);

                var productId = 0;
                
                await _priceManager.CreatePricesAsync(productDto.CurrencyCode, productDto.Price, productId);
            }
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }
    }
}