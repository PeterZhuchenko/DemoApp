using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer(CreateCustomerDto customer)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateCustomer(UpdateCustomerDto customer)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

}