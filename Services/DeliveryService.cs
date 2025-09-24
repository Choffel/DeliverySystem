using DeliverySystem.Abstractions;
using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ApplicationDbContext _context;
    public DeliveryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Создать заказ
    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.CreatedAt = DateTime.UtcNow;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Получить все заказы
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    // Получить заказ по id
    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    // Обновить заказ
    public async Task<Order> UpdateOrderAsync(Guid id, Order updatedOrder)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
            throw new Exception("Order not found");
        order.CustomerId = updatedOrder.CustomerId;
        order.CourierId = updatedOrder.CourierId;
        order.AddressId = updatedOrder.AddressId;
        order.Status = updatedOrder.Status;
        await _context.SaveChangesAsync();
        return order;
    }

    // Удалить заказ
    public async Task<Order> DeleteOrderAsync(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
            throw new Exception("Order not found");
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return order;
    }
}