using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.DTOs;
using DeliverySystem.Enums;

namespace DeliverySystem.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ApplicationDbContext _context;
    
    public DeliveryService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == createOrderDto.CustomerEmail);
        if (customer == null)
        {
            throw new InvalidOperationException("Пользователь с таким email не найден. Сначала создайте Customer.");
        }
        
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = GenerateOrderNumber(),
            CustomerId = customer.Id,
            DeliveryAddress = string.IsNullOrWhiteSpace(createOrderDto.DeliveryAddress) ? customer.Address : createOrderDto.DeliveryAddress,
            Product = createOrderDto.Product,
            CreatedAt = DateTime.UtcNow,
            Status = DeliveryStatus.Pending,
        };
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        
        return MapToResponse(order, null);
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.Include(o => o.Courier).ToListAsync();
        return orders.Select(o => MapToResponse(o, o.Courier)).ToList();
    }
    
    public async Task<OrderResponseDto> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) throw new KeyNotFoundException("Заказ не найден");

        
        order.Status = updateOrderDto.Status;
        if (!string.IsNullOrWhiteSpace(updateOrderDto.DeliveryAddress))
            order.DeliveryAddress = updateOrderDto.DeliveryAddress;
        
        if (updateOrderDto.CourierId.HasValue)
        {
            
            var courierExists = await _context.Couriers.AnyAsync(c => c.Id == updateOrderDto.CourierId.Value);
            if (!courierExists)
                throw new InvalidOperationException("Указанный курьер не найден");
            order.CourierId = updateOrderDto.CourierId.Value;
        }
        
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        
        
        Courier courier = null;
        if (order.CourierId.HasValue)
            courier = await _context.Couriers.FirstOrDefaultAsync(c => c.Id == order.CourierId.Value);
        
        return MapToResponse(order, courier);
    }
    
    public async Task<OrderResponseDto> DeleteOrderAsync(Guid id)
    {
        var order = await _context.Orders.Include(o => o.Courier).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) throw new KeyNotFoundException("Order not found");
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return MapToResponse(order, order.Courier);
    }
    
    public async Task<OrderResponseDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders.Include(o => o.Courier).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) throw new KeyNotFoundException("Order not found");
        return MapToResponse(order, order.Courier);
    }

    private static OrderResponseDto MapToResponse(Order o, Courier courier)
    {
        return new OrderResponseDto
        {
            OrderNumber = o.OrderNumber,
            Product = o.Product,
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            DeliveryAddress = o.DeliveryAddress,
            CourierName = courier?.Name,
            CourierPhone = courier?.Phone,
            EstimatedDeliveryTime = null
        };
    }

    private static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N").Substring(0,6).ToUpper()}";
    }
}