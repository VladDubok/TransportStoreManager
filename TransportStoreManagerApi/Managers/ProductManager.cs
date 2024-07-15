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
        var brand = await _brandManager.GetOrCreate(model.BrandName, model.BrandModel);
        var product = _mapper.Map<Product>(model);
        product.Brand = brand;
        await _productRepository.AddAsync(product);
        await _priceManager.CreatePricesAsync(model.CurrencyCode, model.Price, product.Id);
    }

    public async Task<IEnumerable<GetCustomerProductResponseModel>> GetCustomerProductsAsync(long customerId)
    {
        var customerProducts = await _productRepository.GetCustomerProducts(customerId);

        return _mapper.Map<IEnumerable<GetCustomerProductResponseModel>>(customerProducts);
    }

    public async Task UpdateProductAsync(long id, UpdateProductRequestModel request)
    {
        var brand = await _brandManager.GetOrCreate(request.BrandName, request.BrandModel);
        var product = await _productRepository.GetByIdAsync(id);
        
        if (product is null)
        {
            throw new Exception($"Product with id: {id} was not found");
        }

        product.Color = request.Color;
        product.Year = request.Year;
        product.Count = request.Count;
        product.Brand = brand;
        product.ProductTypeId = request.ProductTypeId;
        await _priceManager.UpdateProductPriceAsync(product.Id, request.Price, request.CurrencyCode);
        

        await _productRepository.UpdateAsync(product);
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
                if (productDto.Id is null)
                {
                    var addProductModel = _mapper.Map<AddProductRequestModel>(productDto);
                    await CreateProductAsync(addProductModel);
                    continue;
                }

                var updateProductModel = _mapper.Map<UpdateProductRequestModel>(productDto);
                await UpdateProductAsync(productDto.Id.Value, updateProductModel);
            }
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }
    }
}