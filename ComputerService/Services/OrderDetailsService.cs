using AutoMapper;
using Castle.Core.Internal;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class OrderDetailsService : BaseEntityService<OrderDetails>, IOrderDetailsService
{
    public OrderDetailsService(ComputerServiceContext context, IValidator<OrderDetails> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<OrderDetails> GetAllOrderDetails(ParametersModel parameters, OrderDetailsSortEnum? sortOrder)
    {
        var orderDetails = FindAll();
        if (sortOrder != null)
        {
            bool asc = (bool)parameters.asc;
            orderDetails = Enum.IsDefined(typeof(OrderDetailsSortEnum), sortOrder)
                ? sortOrder switch
                {
                    OrderDetailsSortEnum.HardwareCharges => asc
                        ? orderDetails.OrderBy(orderDetails => orderDetails.HardwareCharges)
                        : orderDetails.OrderByDescending(orderDetails => orderDetails.HardwareCharges),
                    OrderDetailsSortEnum.ServiceCharges => asc
                        ? orderDetails.OrderBy(orderDetails => orderDetails.ServiceCharges)
                        : orderDetails.OrderByDescending(orderDetails => orderDetails.ServiceCharges),
                }
                : throw new ArgumentException();
        }
        if (!parameters.searchString.IsNullOrEmpty())
        {
            orderDetails = orderDetails.Where(orderDetails => orderDetails.ServiceDescription.Contains(parameters.searchString) ||
                                                              orderDetails.AdditionalInformation.Contains(parameters.searchString));
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

    public async Task UpdateOrderDetailsAsync(OrderDetails orderDetails)
    {
        await ValidateEntityAsync(orderDetails);
        await UpdateAsync(orderDetails);
    }

    public async Task DeleteOrderDetailsAsync(OrderDetails orderDetails)
    {
        await DeleteAsync(orderDetails);
    }
}