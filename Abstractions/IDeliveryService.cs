using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.Models;

namespace DeliverySystem.Abstractions;

public interface IDeliveryService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<List<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto> GetOrderByIdAsync(Guid id);
    Task<OrderResponseDto> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto);
    Task<OrderResponseDto> DeleteOrderAsync(Guid id);
}
