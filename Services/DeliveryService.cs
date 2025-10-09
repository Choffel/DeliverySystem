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
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ApplicationDbContext _context;
    
    public DeliveryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync([FromBody] CreateOrderDto dto)
    {
        var order = new Order()
        {
            Id = Guid.NewGuid(),
            DeliveryAddress = dto.DeliveryAddress,
            Product = dto.Product,
            OrderNumber = Guid.NewGuid().ToString(),
            Status = DeliveryStatus.Pending,
        };
        
        _context.Add(order);
        await _context.SaveChangesAsync();
        
        return order;
    }
    
    public async Task<Order>AssignOrderToCourierAsync(OrderAdmin orderAdmin)
    {
       var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderAdmin.OrderId);

       if (order is null)
       {
           throw new Exception("Order not found");
       }
       
       order.CourierId = orderAdmin.CourierId;
       order.Status = DeliveryStatus.InTransit;
       
       await _context.SaveChangesAsync();
       
       return order;
    }
    
    public async Task<List<CourierOrderDto>> GetOrdersForCourierAsync(Guid courierId)
    {
            return await _context.Orders
                .Where(o => o.CourierId == courierId)
                .Select(o => new CourierOrderDto
                {
                    OrderNumber = o.OrderNumber,
                    DeliveryAddress = o.DeliveryAddress,
                    Status = o.Status
                })
                .ToListAsync();
    }
    
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        return order;
    }
    
    public async Task<Order> UpdateOrderAsync(Guid orderId, UpdateOrderDto dto)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        
        order.DeliveryAddress = dto.DeliveryAddress;
        order.Product = dto.Product;
        order.Status = dto.Status;
        
        await _context.SaveChangesAsync();
        
        return order;
    }
    
    public async Task<Order> DeleteOrderAsync(Guid orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        
        return order;
    }
}