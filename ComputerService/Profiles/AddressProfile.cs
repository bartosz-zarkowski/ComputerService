using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Profiles;
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressViewModel>();
        CreateMap<CreateAddressModel, Address>();
        CreateMap<UpdateAddressModel, Address>();
        CreateMap<Address, UpdateAddressModel>();
    }
}
