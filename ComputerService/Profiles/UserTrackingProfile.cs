﻿using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class UserTrackingProfile : Profile
{
    public UserTrackingProfile()
    {
        CreateMap<UserTracking, UserTrackingViewModel>();
    }
}