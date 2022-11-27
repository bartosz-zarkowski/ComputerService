using AutoMapper;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderViewModel>();
        CreateMap<CreateOrderModel, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatusEnum.Pending));
        CreateMap<UpdateOrderModel, Order>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        CreateMap<Order, UpdateOrderModel>();
    }
}
