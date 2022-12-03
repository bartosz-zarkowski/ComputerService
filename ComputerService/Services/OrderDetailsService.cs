using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;
public class OrderDetailsService : BaseEntityService<OrderDetails>, IOrderDetailsService
{
    public OrderDetailsService(ComputerServiceContext context, IValidator<OrderDetails> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<OrderDetails> GetAllOrderDetails(ParametersModel parameters, OrderDetailsSortEnum? sortOrder)
    {
        var orderDetails = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            orderDetails = sortOrder switch
            {
                OrderDetailsSortEnum.HardwareCharges => asc
                    ? orderDetails.OrderBy(details => details.HardwareCharges)
                    : orderDetails.OrderByDescending(details => details.HardwareCharges),
                OrderDetailsSortEnum.ServiceCharges => asc
                    ? orderDetails.OrderBy(details => details.ServiceCharges)
                    : orderDetails.OrderByDescending(details => details.ServiceCharges),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            orderDetails = orderDetails.Where(details => details.ServiceDescription.Contains(parameters.searchString) ||
                                                         details.AdditionalInformation.Contains(parameters.searchString));
        }
        return orderDetails;
    }

    public async Task<PagedList<OrderDetails>> GetPagedOrderDetailsAsync(ParametersModel parameters, OrderDetailsSortEnum? sortOrder)
    {
        return await PagedList<OrderDetails>.ToPagedListAsync(GetAllOrderDetails(parameters, sortOrder), parameters);
    }

    public async Task<OrderDetails> GetOrderDetailsAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrderDetailsAsync(OrderDetails orderDetails)
    {
        await ValidateEntityAsync(orderDetails);
        await CreateAsync(orderDetails);
    }

    public async Task UpdateOrderDetailsAsync(OrderDetails orderDetails, JsonPatchDocument<UpdateOrderDetailsModel> updateOrderDetailsModelJpd)
    {
        var mappedOrderDetails = Mapper.Map<UpdateOrderDetailsModel>(orderDetails);
        updateOrderDetailsModelJpd.ApplyTo(mappedOrderDetails);
        Mapper.Map(mappedOrderDetails, orderDetails);
        await ValidateEntityAsync(orderDetails);
        await UpdateAsync(orderDetails);
    }

    public async Task DeleteOrderDetailsAsync(OrderDetails orderDetails)
    {
        await DeleteAsync(orderDetails);
    }
}