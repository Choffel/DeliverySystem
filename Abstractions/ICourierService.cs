using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.DTOs.CourierDTOs;

namespace DeliverySystem.Abstractions;

public interface ICourierService
{
    Task<Courier> AddCourierAsync(CourierRegistration dto); 
    Task<List<Courier>> GetAllCouriersAsync();
    Task<Courier> GetCourierByIdAsync(Guid id);
    Task<Courier> ChangeCourierAsync(Guid id, string name, string phone);
    Task<Courier> DeleteCourierAsync(Guid id);
    Task<Courier> LoginAsync(CourierLoginDto courierLoginDto);
    Task<Courier> ResetPasswordAsync(Guid id, string newPassword);
    Task<Courier> ResetPasswordAsync(CourierResetPasswordDto dto);
}
