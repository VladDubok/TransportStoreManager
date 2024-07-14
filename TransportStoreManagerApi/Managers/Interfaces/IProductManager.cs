using TransportStoreManagerApi.Models.Dtos;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;

namespace TransportStoreManagerApi.Managers.Interfaces;

public interface IProductManager
{
    Task CreateProductAsync(AddProductRequestModel model);
    Task<IEnumerable<GetCustomerProductResponseModel>> GetCustomerProductsAsync(long customerId);
    Task UpdateAsync(long id, UpdateProductRequestModel request);
    Task UploadCustomerProductFile(IFormFile file, long customerId);
    Task UpdateFromFile(IEnumerable<FileProductDto> products);
}