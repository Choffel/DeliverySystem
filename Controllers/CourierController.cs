using DeliverySystem.DTOs;
using DeliverySystem.Models;
using DeliverySystem.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/couriers")]
public class CourierController : ControllerBase
{
    private readonly ICourierService _courierService;
    public CourierController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCourierAsync([FromBody] CourierDto courierDto)
    {
        var courier = new Courier
        {
            Name = courierDto.Name,
            Phone = courierDto.Phone
        };
        var createdCourier = await _courierService.AddCourierAsync(courier);
        return Ok(createdCourier);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCouriersAsync()
    {
        var couriers = await _courierService.GetAllCouriersAsync();
        return Ok(couriers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourierByIdAsync([FromRoute] Guid id)
    {
        var courier = await _courierService.GetCourierByIdAsync(id);
        return Ok(courier);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> ChangeCourierAsync([FromRoute] Guid id, [FromBody] CourierDto courierDto)
    {
        var courier = await _courierService.ChangeCourierAsync(id, courierDto.Name, courierDto.Phone);
        return Ok(courier);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourierAsync([FromRoute] Guid id)
    {
        var courier = await _courierService.DeleteCourierAsync(id);
        return Ok(courier);
    }
}