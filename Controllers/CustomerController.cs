using DeliverySystem.Abstractions;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomerAsync([FromBody] Customer customer)
    {
        var created = await _customerService.AddCustomerAsync(customer);
        return Ok(created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomerAsync([FromRoute] Guid id, [FromBody] Customer customer)
    {
        var updated = await _customerService.UpdateCustomerAsync(id, customer);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] Guid id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        return Ok(deleted);
    }
}

