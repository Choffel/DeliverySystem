using DeliverySystem.Abstractions;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.DTOs;
using DeliverySystem.JwtGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly UserManager<IdentityUser> _userManager;

    public CustomerController(
        ICustomerService customerService,
        UserManager<IdentityUser> userManager)
    {
        _customerService = customerService;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCustomerDto dto)
    {
        var customer = await _customerService.CustomerLoginAsync(dto);
        if (customer == null)
            return Unauthorized();

        var user = await _userManager.FindByEmailAsync(customer.Email);
        if (user == null)
        {
            return Unauthorized();
        }
            
        var roles = await _userManager.GetRolesAsync(user);
        var generatedToken = new GenerateJwtToken();
        var token = generatedToken.GenerateToken("", user.Id, roles);
        return Ok(new { token });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> AddCustomerAsync([FromBody] RegistrationCustomerDto dto)
    {
        var result = await _customerService.AddCustomerAsync(dto);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] string email)
    {
        var customer = await _customerService.GetCustomerByEmailAsync(email);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }

    [Authorize(Roles = "Admin,Customer")]
    [HttpPut("update/{email}")]
    public async Task<IActionResult> UpdateCustomerAsync([FromRoute] string email, [FromBody] RegistrationCustomerDto customer)
    {
        var result = await _customerService.UpdateCustomerAsync(email, customer);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{email}")]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] string email)
    {
        var result = await _customerService.DeleteCustomerAsync(email);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok(result);
    }
    
    [HttpGet("Orders/{email}")]
    [Authorize(Roles = "Admin,user")]
    public async Task<IActionResult> GetOrdersByCustomerEmailAsync([FromRoute] string email)
    {
        var orders = await _customerService.GetOrdersByCustomerEmailAsync(email);
        return Ok(orders);
    }
}
