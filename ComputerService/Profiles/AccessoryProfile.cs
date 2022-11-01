﻿using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Profiles;
public class AccessoryProfile : Profile
{
    public AccessoryProfile()
    {
        CreateMap<Accessory, AccessoryViewModel>();
        CreateMap<CreateAccessoryModel, Accessory>();
        CreateMap<UpdateAccessoryModel, Accessory>();
    }
}