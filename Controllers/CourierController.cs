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
    public async Task<IActionResult> AddCourierAsync([FromBody] Courier courier)
    {
        await _courierService.AddCourierAsync(courier);
        return Ok();
    }
}