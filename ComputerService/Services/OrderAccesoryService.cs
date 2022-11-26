using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;
public class OrderAccessoryService : BaseEntityService<OrderAccessory>, IOrderAccessoryService
{
    public OrderAccessoryService(ComputerServiceContext context, IValidator<OrderAccessory> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<OrderAccessory> GetAllOrderAccessories(ParametersModel parameters, OrderAccessorySortEnum? sortOrder)
    {
        var orderAccessories = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            orderAccessories = sortOrder switch
            {
                OrderAccessorySortEnum.CreatedAt => asc
                    ? orderAccessories.OrderBy(orderAccessory => orderAccessory.CreatedAt)
                    : orderAccessories.OrderByDescending(orderAccessory => orderAccessory.CreatedAt),
                OrderAccessorySortEnum.UpdatedAt => asc
                    ? orderAccessories.OrderBy(orderAccessory => orderAccessory.UpdatedAt)
                    : orderAccessories.OrderByDescending(orderAccessory => orderAccessory.UpdatedAt),
                OrderAccessorySortEnum.Name => asc
                    ? orderAccessories.OrderBy(orderAccessory => orderAccessory.Name)
                    : orderAccessories.OrderByDescending(orderAccessory => orderAccessory.Name),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            orderAccessories = orderAccessories.Where(orderAccessory =>
                orderAccessory.Name.Contains(parameters.searchString));
        }
        return orderAccessories;
    }

    public async Task<PagedList<OrderAccessory>> GetPagedOrderAccessoriesAsync(ParametersModel parameters, OrderAccessorySortEnum? sortOrder)
    {
        return await PagedList<OrderAccessory>.ToPagedListAsync(GetAllOrderAccessories(parameters, sortOrder), parameters);
    }

    public async Task<OrderAccessory> GetOrderAccessoryAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await ValidateEntityAsync(orderAccessory);
        await CreateAsync(orderAccessory);
    }

    public async Task UpdateOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await ValidateEntityAsync(orderAccessory);
        await UpdateAsync(orderAccessory);
    }

    public async Task DeleteOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await DeleteAsync(orderAccessory);
    }
}