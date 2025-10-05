    using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.DTOs;
using DeliverySystem.JwtGenerator;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Services;

public class CourierService : ICourierService
{
    private readonly ApplicationDbContext _context;
    public CourierService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Courier> LoginAsync([FromBody] CourierLoginDto courierLoginDto)
    {
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Email == courierLoginDto.Email);
        if (courier == null || courier.Password != courierLoginDto.Password)
        {
            throw new Exception("Invalid email or password");
        }

        return courier;
    }

    public async Task<Courier> AddCourierAsync([FromBody] CourierRegistration dto)
    {
        var courier = new Courier{
            Id = Guid.NewGuid(),
            Name = dto.FullName,
            Phone = dto.Phone,
            Email = dto.Email,
            Password = dto.Password
        };
        _context.Add(courier);
        await _context.SaveChangesAsync();
        
        return courier;
    }

    public async Task<List<Courier>> GetAllCouriersAsync()
    {
        return await _context.Couriers.ToListAsync();
    }

    public async Task<Courier> GetCourierByIdAsync(Guid id)
    {
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Id == id);
        if (courier == null)
        {
            throw new Exception("Courier not found");
        }
        return courier;
    }

    public async Task<Courier> ChangeCourierAsync(Guid id, string name, string phone)
    {
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Id == id);
        if (courier == null)
        {
            throw new Exception("Courier not found");
        }
        courier.Name = name;
        courier.Phone = phone;
        await _context.SaveChangesAsync();
        return courier;
    }

    public async Task<Courier> DeleteCourierAsync(Guid id)
    {
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Id == id);
        if (courier == null)
        {
            throw new Exception("Courier not found");
        }
        _context.Couriers.Remove(courier);
        await _context.SaveChangesAsync();
        return courier;
    }
}
