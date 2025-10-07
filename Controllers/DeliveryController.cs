using DeliverySystem.Abstractions;
using DeliverySystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DeliverySystem.Models;
using System.Collections.Generic;

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

    // Создание заказа (только нужные поля от клиента)
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrderDto)
    {
        var order = await _deliveryService.CreateOrderAsync(createOrderDto);
        return Created($"api/orders/number/{order.OrderNumber}", order);
    }

    // Назначение заказа курьеру (для администратора)
    [HttpPost("assign")]
    public async Task<IActionResult> AssignOrderToCourierAsync([FromBody] OrderAdmin orderAdmin)
    {
        var order = await _deliveryService.AssignOrderToCourierAsync(orderAdmin);
        return Ok(order);
    }

    // Получить все заказы
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var orders = await _deliveryService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Получить все заказы для курьера
    [HttpGet("courier/{courierId:guid}")]
    public async Task<IActionResult> GetOrdersForCourierAsync([FromRoute] Guid courierId)
    {
        var orders = await _deliveryService.GetOrdersForCourierAsync(courierId);
        return Ok(orders);
    }

    // Получить заказ по Id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid id)
    {
        var order = await _deliveryService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    // Обновить заказ
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid id, [FromBody] UpdateOrderDto updateOrderDto)
    {
        var order = await _deliveryService.UpdateOrderAsync(id, updateOrderDto);
        return Ok(order);
    }

    // Удалить заказ
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid id)
    {
        var order = await _deliveryService.DeleteOrderAsync(id);
        return Ok(order);
    }
}