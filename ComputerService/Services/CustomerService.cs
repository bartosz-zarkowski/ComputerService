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

public class CustomerService : BaseEntityService<Customer>, ICustomerService
{
    public CustomerService(ComputerServiceContext context, IValidator<Customer> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Customer> GetAllCustomers(ParametersModel parameters, CustomerSortEnum? sortOrder)
    {
        var customers = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            customers = sortOrder switch
            {
                CustomerSortEnum.CreatedAt => asc
                    ? customers.OrderBy(customer => customer.CreatedAt)
                    : customers.OrderByDescending(customer => customer.CreatedAt),
                CustomerSortEnum.UpdatedAt => asc
                    ? customers.OrderBy(customer => customer.UpdatedAt)
                    : customers.OrderByDescending(customer => customer.UpdatedAt),
                CustomerSortEnum.FirstName => asc
                    ? customers.OrderBy(customer => customer.FirstName)
                    : customers.OrderByDescending(customer => customer.FirstName),
                CustomerSortEnum.LastName => asc
                    ? customers.OrderBy(customer => customer.LastName)
                    : customers.OrderByDescending(customer => customer.LastName),
                CustomerSortEnum.Email => asc
                    ? customers.OrderBy(customer => customer.Email)
                    : customers.OrderByDescending(customer => customer.Email),
                CustomerSortEnum.PhoneNumber => asc
                    ? customers.OrderBy(customer => customer.PhoneNumber)
                    : customers.OrderByDescending(customer => customer.PhoneNumber),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            customers = customers.Where(customer => customer.FirstName.Contains(parameters.searchString) ||
                                              customer.LastName.Contains(parameters.searchString) ||
                                              customer.Email.Contains(parameters.searchString) ||
                                              customer.PhoneNumber.Contains(parameters.searchString));
        }
        return customers;
    }

    public async Task<PagedList<Customer>> GetPagedCustomersAsync(ParametersModel parameters, CustomerSortEnum? sortOrder)
    {
        return await PagedList<Customer>.ToPagedListAsync(GetAllCustomers(parameters, sortOrder), parameters);
    }

    public async Task<Customer> GetCustomerAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await ValidateEntityAsync(customer);
        await CreateAsync(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer, JsonPatchDocument<UpdateCustomerModel> updateCustomerModelJpd)
    {
        var mappedCustomer = Mapper.Map<UpdateCustomerModel>(customer);
        updateCustomerModelJpd.ApplyTo(mappedCustomer);
        Mapper.Map(mappedCustomer, customer);
        await ValidateEntityAsync(customer);
        await UpdateAsync(customer);
    }

    public async Task DeleteCustomerAsync(Customer customer)
    {
        await DeleteAsync(customer);
    }
}