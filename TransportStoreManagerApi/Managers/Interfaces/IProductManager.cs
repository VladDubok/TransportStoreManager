using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;

namespace TransportStoreManagerApi.Managers;

public interface IProductManager
{
    Task CreateProductAsync(AddProductRequestModel model);
    Task<IEnumerable<GetCustomerProductResponseModel>> GetCustomerProducts(long customerId);
}