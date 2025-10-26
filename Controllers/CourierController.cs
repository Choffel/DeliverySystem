using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Identity;

using DeliverySystem.Abstractions;
using DeliverySystem.JwtGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.DTOs.CourierDTOs;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/couriers")]
public class CourierController : ControllerBase
{
    private readonly ICourierService _courierService;
    private readonly UserManager<IdentityUser> _userManager;
   
    public CourierController(ICourierService courierService, UserManager<IdentityUser> userManager)
    {
        _courierService = courierService;
        _userManager = userManager;
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

        var user = await _userManager.FindByEmailAsync(courier.Email);
        var roles = user is null ? new List<string>() : (await _userManager.GetRolesAsync(user));

        var generatedToken = new GenerateJwtToken();
        var token = generatedToken.GenerateToken("", courier.Id.ToString(), roles);
        return Ok(new { token });
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddCourierAsync([FromBody] CourierRegistration dto)
    {
        var created = await _courierService.AddCourierAsync(dto);
        
        return Ok(created);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllCouriersAsync()
    {
        var couriers = await _courierService.GetAllCouriersAsync();
        return Ok(couriers);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourierByIdAsync([FromRoute] Guid id)
    {
        var courier = await _courierService.GetCourierByIdAsync(id);
        return Ok(courier);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> ChangeCourierAsync([FromRoute] Guid id, [FromBody] CourierDto courierDto)
    {
        var courier = await _courierService.ChangeCourierAsync(id, courierDto.Name, courierDto.Phone);
        return Ok(courier);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourierAsync([FromRoute] Guid id)
    {
        var courier = await _courierService.DeleteCourierAsync(id);
        return Ok(courier);
    }

    [Authorize(Roles = "Admin,Courier")]
    [HttpPut("{id:guid}/reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromRoute] Guid id, [FromBody] CourierResetPasswordDto dto)
    {
        var courier = await _courierService.ResetPasswordAsync(id, dto.NewPassword);
        return Ok(courier);
    }
}