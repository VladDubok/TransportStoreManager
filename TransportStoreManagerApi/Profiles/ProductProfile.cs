using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Dtos;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Models.Responses;

namespace TransportStoreManagerApi.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestModel, Product>();
        CreateMap<ProductPrice, ProductPriceModel>();

        CreateMap<Product, GetCustomerProductResponseModel>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.BrandModel, opt => opt.MapFrom(src => src.Brand.Model))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.ProductPrices));

        CreateMap<UploadFileProductDto, AddProductRequestModel>();
        CreateMap<UploadFileProductDto, UpdateProductRequestModel>();
    }
}