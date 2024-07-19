using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Repositories.Interfaces;

namespace TransportStoreManagerApi.Managers;

public class PromotionManager : IPromotionManager
{
    private readonly IBaseRepository<Promotion> _promotionRepository;
    private readonly IBaseRepository<ProductPromotionHistory> _promotionHistoryRepository;
    private readonly IBaseRepository<ProductPromotion> _productPromotionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public PromotionManager(
        IMapper mapper,
        IBaseRepository<Promotion> promotionRepository,
        IBaseRepository<ProductPromotionHistory> promotionHistoryRepository,
        IBaseRepository<ProductPromotion> productPromotionRepository,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _promotionRepository = promotionRepository;
        _promotionHistoryRepository = promotionHistoryRepository;
        _productPromotionRepository = productPromotionRepository;
        _productRepository = productRepository;
    }

    public async Task CreatePromotionAsync(CreatePromotionRequest request)
    {
        var newPromotion = _mapper.Map<Promotion>(request);
        await _promotionRepository.AddAsync(newPromotion);
    }

    public async Task UpdatePromotionAsync(UpdatePromotionRequest request)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.Id);

        if (promotion is null)
        {
            throw new Exception($"Promotion with id: {request.Id} was not found");
        }

        promotion.Percent = request.Percent;
        promotion.StartDate = request.StartDate;
        promotion.EndDate = request.EndDate;

        if (promotion.ProductPromotions.Any())
        {
            foreach (var productPromotion in promotion.ProductPromotions)
            {
                productPromotion.IsActive = request.IsActive;
                await _productPromotionRepository.UpdateAsync(productPromotion);
                
                var history = _mapper.Map<ProductPromotionHistory>(request);
                history.ProductPromotionId = productPromotion.Id;
                history.Created = DateTime.UtcNow;
                await _promotionHistoryRepository.AddAsync(history);
            }
        }

        await _promotionRepository.UpdateAsync(promotion);
    }

    public async Task AssignToProductsAsync(AssignPromotionToProductRequest request)
    {
        var products = await _productRepository.GetByIdsAsync(request.ProductIds);
        
        if (products.Count != request.ProductIds.Count())
        {
            var notFoundProductIds = request.ProductIds.Except(products.Select(x => x.Id).ToList());
            Log.Error($"Products does not exist with ids: {string.Join(", ", notFoundProductIds)}");
            throw new Exception($"Products does not exist with ids: {string.Join(", ", notFoundProductIds)}");
        }

        var promotion = await _promotionRepository.GetByIdAsync(request.PromotionId);

        if (promotion is null)
        {
            throw new Exception($"Promotion with id: {request.PromotionId} was not found");
        }

        var newProductPromotions = new List<ProductPromotion>();
        var dateTimeNow = DateTime.UtcNow;
        foreach (var product in products)
        {
            var productPromotion = new ProductPromotion
            {
                PromotionId = promotion.Id,
                ProductId = product.Id,
                IsActive = true,
               ProductPromotionHistories = new List<ProductPromotionHistory>()
            };
            var newProductHistoryPromotion = _mapper.Map<ProductPromotionHistory>(promotion);
            newProductHistoryPromotion.Created = dateTimeNow;
            newProductHistoryPromotion.IsActive = productPromotion.IsActive;
            productPromotion.ProductPromotionHistories.Add(newProductHistoryPromotion);
            newProductPromotions.Add(productPromotion);
        }

        await _productPromotionRepository.AddRangeAsync(newProductPromotions);
    }

    public async Task<IEnumerable<Promotion>> GetAllAsync()
    {
        return await _promotionRepository.GetAll().ToListAsync();
    }

    public async Task ActivatePromotionProductAsync(ActivatePromotionProductRequest request)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.PromotionId);

        if (promotion is null)
        {
            throw new Exception($"Promotion with id: {request.PromotionId} was not found");
        }
        
        var productPromotions = await _productPromotionRepository.GetAll()
            .Include(x => x.ProductPromotionHistories)
            .Where(x => request.PromotionId == x.PromotionId &&
                        request.ProductIds.ToList().Contains(x.ProductId))
            .ToListAsync();

        if (productPromotions.Count != request.ProductIds.Count())
        {
            var notFoundProductPromotionsIds = request.ProductIds.Except(productPromotions.Select(x => x.Id).ToList());
            Log.Error($"Promotion with Id:{request.PromotionId}, are not assigned to Products with ids: {string.Join(", ", notFoundProductPromotionsIds)}");
            throw new Exception($"Promotion with Id:{request.PromotionId}, are not assigned to Products with ids: {string.Join(", ", notFoundProductPromotionsIds)}");
        }
        
        foreach (var productPromotion in productPromotions)
        {
            productPromotion.IsActive = request.Activate;
            var history = _mapper.Map<ProductPromotionHistory>(promotion);
            productPromotion.ProductPromotionHistories.Add(history);
        }

        await _productPromotionRepository.UpdateRangeAsync(productPromotions);
    }
}