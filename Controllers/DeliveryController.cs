using DeliverySystem.Abstractions;
using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/orders")] // Базовый маршрут
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrderDto)
    {
        var order = await _deliveryService.CreateOrderAsync(createOrderDto);
        return Created($"api/orders/number/{order.OrderNumber}", order);
    }

    
    [HttpGet("number/{orderNumber}")]
    public async Task<IActionResult> GetByOrderNumberAsync([FromRoute] string orderNumber)
    {
        
        var all = await _deliveryService.GetAllOrdersAsync();
        var found = all.Find(o => o.OrderNumber == orderNumber);
        if (found == null) return NotFound();
        return Ok(found);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var orders = await _deliveryService.GetAllOrdersAsync();
        return Ok(orders);
    }

    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid id)
    {
        var order = await _deliveryService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid id, [FromBody] UpdateOrderDto updateOrderDto)
    {
        var order = await _deliveryService.UpdateOrderAsync(id, updateOrderDto);
        return Ok(order);
    }

    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid id)
    {
        var order = await _deliveryService.DeleteOrderAsync(id);
        return Ok(order);
    }
}