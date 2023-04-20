using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<Customer, CreateCustomerDto>();
        
        CreateMap<UpdateCustomerDto, Customer>();
        
    }
}