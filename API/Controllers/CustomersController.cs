using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<Customer>>> GetCustomers([FromQuery]UserParams userParams)
    {
        var customers = await _customersService.GetCustomers(userParams);
        
        Response.AddPaginationHeader(new PaginationHeader(customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages));

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        var customer = await _customersService.GetCustomerById(id);

        if (customer == null)
        {
            return NotFound("Customer not found.");
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task CreateCustomer(CreateCustomerDto customer)
    {
        await _customersService.CreateCustomer(customer);
    }
    
    [HttpPut]
    public async Task UpdateCustomer(UpdateCustomerDto customer)
    {
        await _customersService.UpdateCustomer(customer);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteCustomer(Guid id)
    {
        await _customersService.DeleteCustomer(id);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Customer>>> Search([FromQuery]string searchString)
    {
        var result = await _customersService.Search(searchString);

        return Ok(result);
    }

}