using API.Data;
using API.DTOs;
using API.Entities;
using API.ErrorHandling;
using API.Helpers;
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

    public async Task<PagedList<Customer>> GetCustomers(UserParams userParams)
    {
        var query =  _context.Customers
            .AsNoTracking();

        return await PagedList<Customer>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
    }

    public async Task<Customer> GetCustomerById(Guid id)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
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
        var isExisted = await _context.Customers.AnyAsync(x => x.Id == customer.Id);

        if (isExisted)
        {
            var mappedCustomer = _mapper.Map<Customer>(customer);
            _context.Update(mappedCustomer);
            await _context.SaveChangesAsync();
        }

        throw new NotFoundException("Customer not found");
    }

    public async Task DeleteCustomer(Guid id)
    {
        var customer = await _context.Customers
            .SingleOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
        }
        
        _context.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Customer>> Search(string query)
    {
        var stringProperties = typeof(Customer).GetProperties().Where(prop =>
            prop.PropertyType == query.GetType());
       
        // Super variant, but does not work. Fails with exception
        //System.ArgumentException: Expression of type 'System.String' cannot be used for parameter of type 'System.Reflection.PropertyInfo' of method 'Boolean Contains[PropertyInfo](System.Collections.Generic.IEnumerable`1[System.Reflection.PropertyInfo], System.Reflection.PropertyInfo)' (Parameter 'arg1')
        var res = await  _context.Customers.Where(customer => 
            stringProperties.Any(prop => (string) prop.GetValue(customer)! == query)).ToListAsync();
        
        //This variant works, but what if I have 1billion customers. It can be problem.
        var customers = await _context.Customers.ToArrayAsync();
        var searchedCustomers = customers.Where(customer => stringProperties.Any(prop => (string) prop.GetValue(customer)! == query));

        // Initial variant. Works fine, but violates OCP. Every time I change customer entity I will have to change this query.
        var result = await _context.Customers.Where(x =>
            x.Name == query ||
            x.CompanyName == query ||
            x.Email.Contains(query) ||
            x.Phone == query)
            .ToListAsync();

        return result;
    }

   
}