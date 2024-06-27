using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<AddCustomerRequestModel, Customer>();
    }
}