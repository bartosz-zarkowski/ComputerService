using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();

        CreateMap<CreateUserModel, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        CreateMap<UpdateUserModel, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        CreateMap<User, UpdateUserModel>();

        CreateMap<User, AuthenticateRequestModel>();

        CreateMap<User, JwtUserModel>();
    }
}