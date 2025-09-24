using DeliverySystem.Models;
using DeliverySystem.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DeliverySystem.Controllers;

[ApiController]
[Route("api/delivery")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;
    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] Order order)
    {
        var createdOrder = await _deliveryService.CreateOrderAsync(order);
        return Ok(createdOrder);
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrdersAsync()
    {
        var orders = await _deliveryService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderByIdAsync(Guid id)
    {
        var order = await _deliveryService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderAsync(Guid id, [FromBody] Order order)
    {
        var updatedOrder = await _deliveryService.UpdateOrderAsync(id, order);
        return Ok(updatedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        var deletedOrder = await _deliveryService.DeleteOrderAsync(id);
        return Ok(deletedOrder);
    }
}