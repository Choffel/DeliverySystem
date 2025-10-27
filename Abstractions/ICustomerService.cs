using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Identity;

namespace DeliverySystem.Abstractions;

public interface ICustomerService
{
    Task<RegistrationCustomerDto> GetCustomerByEmailAsync(string email);
    Task<List<RegistrationCustomerDto>> GetAllCustomersAsync();
    Task<IdentityResult> AddCustomerAsync(RegistrationCustomerDto dto);
    Task<RegistrationCustomerDto> CustomerLoginAsync(LoginCustomerDto dto);
    Task<IdentityResult> UpdateCustomerAsync(string email, RegistrationCustomerDto customer);
    Task<IdentityResult> DeleteCustomerAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string email, string NewPassword);
    
    Task<List<Order>> GetOrdersByCustomerEmailAsync(string email);
}