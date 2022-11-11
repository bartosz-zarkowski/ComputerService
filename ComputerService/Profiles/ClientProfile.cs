using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientViewModel>();
        CreateMap<CreateClientModel, Client>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        CreateMap<UpdateClientModel, Client>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
    }
}
