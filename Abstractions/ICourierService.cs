using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Abstractions;

public interface ICourierService
{
    Task<Courier> AddCourierAsync(Courier courier);
    Task<List<Courier>> GetAllCouriersAsync();
    Task<Courier> GetCourierByIdAsync(Guid id);
    Task<Courier> ChangeCourierAsync(Guid id, string name, string phone);
    Task<Courier> DeleteCourierAsync(Guid id);
    Task<Courier>LoginAsync(CourierLoginDto courierLoginDto);
}

