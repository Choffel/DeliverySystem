using DeliverySystem.DTOs;
using DeliverySystem.Models;
using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.JwtGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] CourierLoginDto courierLoginDto)
    {
        var courier = await _courierService.LoginAsync(courierLoginDto);
        if (courier == null)
        {
            return Unauthorized();
        }
        var generatedToken = new GenerateJwtToken();
        var token = generatedToken.GenerateToken("", courier.Id.ToString());
        return Ok(new { token });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCourierAsync([FromBody] CourierRegistration dto)
    {
        var Courier = new Courier
        {
            Name = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.Password
        };
        var created = await _courierService.AddCourierAsync(Courier);
        return Ok(created);
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