using DeliverySystem.DTOs;
using DeliverySystem.Models;
using DeliverySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers;

public class CourierController : ControllerBase
{
    private readonly CourierService _courierService;
    
    
    public CourierController(CourierService courierService)
    {
        _courierService = courierService;
    }
    
    
    [HttpPost("/couriers")]
    public async Task<IActionResult> AddCourierAsync([FromBody] CourierDto courierDto)
    {
        var courier = new Courier
        {
            Name = courierDto.Name,
            Phone = courierDto.Phone
        };
        await _courierService.AddCourierAsync(courier);
        return Ok();
    }

    [HttpGet("/couriers")]
    public async Task<IActionResult> GetAllCouriersAsync()
    {
      var couriers = await _courierService.GetAllCouriersAsync();
      
      return Ok(couriers);
    }
}