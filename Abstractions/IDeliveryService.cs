using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.Models;

namespace DeliverySystem.Abstractions;

public interface IDeliveryService
{
    Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto, string UserId);
    
    Task<Order> AssignOrderToCourierAsync(OrderAdmin orderAdmin);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Order> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto);
    Task<Order> DeleteOrderAsync(Guid id);
    Task<List<CourierOrderDto>> GetOrdersForCourierAsync(Guid courierId);
    
    Task<Order>DeliveryConfirmationAsync(Guid id);
    
    Task<Order> CancelOrderAsync(Guid id);
}
