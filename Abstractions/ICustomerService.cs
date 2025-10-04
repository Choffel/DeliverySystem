using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;

namespace DeliverySystem.Abstractions;

public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(Guid customerId);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> AddCustomerAsync(RegistrationCustomerDto dto);
    
    Task<Customer> CustomerLoginAsync(LoginCustomerDto dto);
    Task<Customer> UpdateCustomerAsync(Guid customerId, Customer customer);
    Task<Customer> DeleteCustomerAsync(Guid customerId);
}