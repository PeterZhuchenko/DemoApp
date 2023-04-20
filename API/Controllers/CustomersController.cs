using API.DTOs;
using API.Entities;
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
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var customers = await _customersService.GetCustomers();

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
    public async Task<ActionResult<IEnumerable<Customer>>> Search(string searchString)
    {
        var result = await _customersService.Search(searchString);

        return Ok(result);
    }

}