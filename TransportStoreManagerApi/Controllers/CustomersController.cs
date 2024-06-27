using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Models.Requests;
using TransportStoreManagerApi.Repositories;

namespace TransportStoreManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IBaseRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(IMapper mapper, IBaseRepository<Customer> customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var customers = await _customerRepository.GetAllAsync();

        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] AddCustomerRequestModel model)
    {
        var newCustomer = _mapper.Map<Customer>(model);
        await _customerRepository.AddAsync(newCustomer);

        return Ok();
    }
}