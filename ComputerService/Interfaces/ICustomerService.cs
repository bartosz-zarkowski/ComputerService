using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface ICustomerService
{
    IQueryable<Customer> GetAllCustomers(ParametersModel parameters, CustomerSortEnum? sortOrder);
    Task<PagedList<Customer>> GetPagedCustomersAsync(ParametersModel parameters, CustomerSortEnum? sortOrder);
    Task<Customer> GetCustomerAsync(Guid id);
    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer, JsonPatchDocument<UpdateCustomerModel> updateCustomerModelJpd);
    Task DeleteCustomerAsync(Customer customer);
}