using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.DTOs;
using DeliverySystem.DTOs.CourierDTOs;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Services;

public class CourierService : ICourierService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public CourierService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Courier> LoginAsync(CourierLoginDto courierLoginDto)
    {
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Email == courierLoginDto.Email);
        if (courier == null || courier.Password != courierLoginDto.Password)
        {
            throw new Exception("Invalid email or password");
        }

        return courier;
    }

    public async Task<Courier> AddCourierAsync(CourierRegistration dto)
    {
      
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.Phone
        };

        var identityResult = await _userManager.CreateAsync(user, dto.Password);
        if (!identityResult.Succeeded)
        {
            throw new Exception("Failed to create identity user for courier: " + string.Join("; ", identityResult.Errors.Select(e => e.Description)));
        }
        
        await _userManager.AddToRoleAsync(user, "Courier");

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
    
    public async Task<Courier> ResetPasswordAsync(CourierResetPasswordDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required for this reset method.", nameof(dto.Email));

        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Email == dto.Email);
        if (courier == null)
        {
            throw new Exception("Courier not found");
        }

        courier.Password = dto.NewPassword;
        await _context.SaveChangesAsync();
        return courier;
    }
}
