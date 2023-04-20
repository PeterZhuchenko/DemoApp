using API.DTOs;
using API.Entities;

namespace API.Services.Interfaces;

public interface ICustomersService
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer> GetCustomerById(Guid id);
    Task CreateCustomer(CreateCustomerDto customer);
    Task UpdateCustomer(UpdateCustomerDto customer);
    Task DeleteCustomer(Guid id);
    
    Task<IEnumerable<Customer>> Search(string query);

}