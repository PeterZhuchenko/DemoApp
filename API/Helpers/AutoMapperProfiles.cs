using API.DTOs;
using API.Entities;
using AutoMapper;


namespace DatingApp.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<Customer, CreateCustomerDto>();
        
        CreateMap<UpdateCustomerDto, Customer>();
        
    }
}