﻿using AutoMapper;
using ComputerService.Entities;
using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Profiles;
public class OrderDetailsProfile : Profile
{
    public OrderDetailsProfile()
    {
        CreateMap<OrderDetails, OrderDetailsViewModel>();
        CreateMap<CreateOrderDetailsModel, OrderDetails>();
        CreateMap<UpdateOrderDetailsModel, OrderDetails>();
        CreateMap<OrderDetails, UpdateOrderDetailsModel>();
    }
}
