using DeliverySystem.Abstractions;
using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.Models;


namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/orders")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrderDto, string UserId)
    {
        var order = await _deliveryService.CreateOrderAsync(createOrderDto, UserId);
        return Created("Created", order);
    }

    
    [HttpPost("assign")]
    public async Task<IActionResult> AssignOrderToCourierAsync([FromBody] OrderAdmin orderAdmin)
    {
        var order = await _deliveryService.AssignOrderToCourierAsync(orderAdmin);
        return Ok(order);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var orders = await _deliveryService.GetAllOrdersAsync();
        return Ok(orders);
    }

    
    [HttpGet("courier/{courierId:guid}")]
    public async Task<IActionResult> GetOrdersForCourierAsync([FromRoute] Guid courierId)
    {
        var orders = await _deliveryService.GetOrdersForCourierAsync(courierId);
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

    [HttpPost("confirm/{id:guid}")]
    public async Task<IActionResult> DeliveryConfirmationAsync([FromRoute] Guid id)
    {
        var result = await _deliveryService.DeliveryConfirmationAsync(id);
        return Ok(result);
    }

    [HttpPost("cancel/{id:guid}")]
    public async Task<IActionResult> CancelOrderAsync([FromRoute] Guid id)
    {
        var result = await _deliveryService.CancelOrderAsync(id);
        return Ok(result);
    }
}