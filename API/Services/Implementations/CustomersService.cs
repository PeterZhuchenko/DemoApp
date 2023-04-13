using API.Data;
using API.DTOs;
using API.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations;

public class CustomersService : ICustomersService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomersService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerById(Guid id)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        return customer;
    }

    public async Task CreateCustomer(CreateCustomerDto customer)
    {
        var mappedCustomer = _mapper.Map<Customer>(customer);
        await _context.Customers.AddAsync(mappedCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomer(UpdateCustomerDto customer)
    {
        var mappedCustomer = _mapper.Map<Customer>(customer);
        _context.Update(mappedCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        
        _context.Remove(customer);
        await _context.SaveChangesAsync();
    }
}