using AutoMapper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;

namespace TransportStoreManagerApi.Profiles;

public class PromotionProfile : Profile
{
    public PromotionProfile()
    {
        CreateMap<CreatePromotionRequest, Promotion>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Promotion, ProductPromotionHistory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.ProductPromotionId, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        
        CreateMap<UpdatePromotionRequest, ProductPromotionHistory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.ProductPromotionId, opt => opt.Ignore());

        CreateMap<ProductPromotion, ProductPromotionHistory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.ProductPromotionId, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
    }
}