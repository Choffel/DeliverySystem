using DeliverySystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Abstractions;

public interface IDeliveryService
{
    Task<Order> CreateOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Order> UpdateOrderAsync(Guid id, Order order);
    Task<Order> DeleteOrderAsync(Guid id);
}
