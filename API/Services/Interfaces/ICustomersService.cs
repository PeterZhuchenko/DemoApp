using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Services.Interfaces;

public interface ICustomersService
{
    Task<PagedList<Customer>> GetCustomers(UserParams userParams);
    Task<Customer> GetCustomerById(Guid id);
    Task CreateCustomer(CreateCustomerDto customer);
    Task UpdateCustomer(UpdateCustomerDto customer);
    Task DeleteCustomer(Guid id);
    
    Task<IEnumerable<Customer>> Search(string query);

}