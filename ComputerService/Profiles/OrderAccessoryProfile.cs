using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class OrderAccessoryProfile : Profile
{
    public OrderAccessoryProfile()
    {
        CreateMap<OrderAccessory, OrderAccessoryViewModel>();
        CreateMap<CreateOrderAccessoryModel, OrderAccessory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        CreateMap<UpdateOrderAccessoryModel, OrderAccessory>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
    }
}
