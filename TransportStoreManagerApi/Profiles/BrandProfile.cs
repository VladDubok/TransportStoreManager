using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<AddBrandRequestModel, Brand>();
    }
}