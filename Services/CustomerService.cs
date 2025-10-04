using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.JwtGenerator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<Customer> CustomerLoginAsync([FromBody] LoginCustomerDto dto)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == dto.Email);
        if (customer == null || customer.Password != dto.Password)
        {
            throw new Exception("Invalid email or password");
        }

        return customer;
    }

    public async Task<Customer> AddCustomerAsync([FromBody] RegistrationCustomerDto dto)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            Password = dto.Password
        };
        
        _context.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
    {
        return await _context.Set<Customer>().FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task<Customer> UpdateCustomerAsync(Guid customerId, Customer customer)
    {
        var existing = await _context.Set<Customer>().FirstOrDefaultAsync(c => c.Id == customerId);
        if (existing == null) throw new Exception("Customer not found");
        existing.Name = customer.Name;
        existing.Phone = customer.Phone;
        existing.Email = customer.Email;
        existing.Address = customer.Address;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Customer> DeleteCustomerAsync(Guid customerId)
    {
        var customer = await _context.Set<Customer>().FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer == null) throw new Exception("Customer not found");
        _context.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
}