using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Repositories;

namespace TransportStoreManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBaseRepository<Brand> _brandRepository;
    private readonly IMapper _mapper;

    public BrandsController(IMapper mapper, IBaseRepository<Brand> brandRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandRepository.GetAllAsync();

        return Ok(brands);
    }

    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] AddBrandRequestModel model)
    {
        var newBrand = _mapper.Map<Brand>(model);
        await _brandRepository.AddAsync(newBrand);

        return Ok();
    }
}