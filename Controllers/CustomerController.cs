using DeliverySystem.Abstractions;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.DTOs;
using DeliverySystem.JwtGenerator;
using Microsoft.AspNetCore.Identity;

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
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCustomerDto dto)
    {
        var customer = await _customerService.CustomerLoginAsync(dto);
        
        if (customer == null)
        {
            return Unauthorized();
        }
        
        // Find the user ID from the email
        var user = await _userManager.FindByEmailAsync(customer.Email);
        if (user is null)
        {
            return Unauthorized();
        }
        
        var generatedToken = new GenerateJwtToken();
        var token = generatedToken.GenerateToken("", user.Id);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> AddCustomerAsync([FromBody] RegistrationCustomerDto dto)
    {
        var result = await _customerService.AddCustomerAsync(dto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] string email)
    {
        var customer = await _customerService.GetCustomerByEmailAsync(email);
        
        return Ok(customer);
    }

    [HttpPut("update {email}")]
    public async Task<IActionResult> UpdateCustomerAsync([FromRoute] string email, [FromBody] RegistrationCustomerDto customer)
    {
        var result = await _customerService.UpdateCustomerAsync(email, customer);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }

    [HttpDelete("delete {email}")]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] string email)
    {
        var result = await _customerService.DeleteCustomerAsync(email);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }
}
