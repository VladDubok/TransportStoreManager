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
        CreateMap<ProductPromotion, ProductPromotionModel>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Promotion.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Promotion.EndDate))
            .ForMember(dest => dest.Percent, opt => opt.MapFrom(src => src.Promotion.Percent));

        CreateMap<Product, GetCustomerProductResponseModel>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.BrandModel, opt => opt.MapFrom(src => src.Brand.Model))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.ProductPrices))
            .ForMember(dest => dest.Promotions, opt => opt.MapFrom(src => src.ProductPromotions));

        CreateMap<UploadFileProductDto, AddProductRequestModel>();
        CreateMap<UploadFileProductDto, UpdateProductRequestModel>();
    }
}