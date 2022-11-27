using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class DeviceProfile : Profile
{
    public DeviceProfile()
    {
        CreateMap<Device, DeviceViewModel>();
        CreateMap<CreateDeviceModel, Device>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        CreateMap<UpdateDeviceModel, Device>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        CreateMap<Device, UpdateDeviceModel>();
    }
}
