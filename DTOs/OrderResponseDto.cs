using System;
using DeliverySystem.Enums;
using DeliverySystem.Models;

namespace DeliverySystem.DTOs;

public class OrderResponseDto
{
    public string OrderNumber { get; set; }
    public string Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DeliveryStatus Status { get; set; }
    public string DeliveryAddress { get; set; }
    
    public string CourierName { get; set; }
    public string CourierPhone { get; set; }
    
    // Ожидаемое время доставки (можно добавить позже)
    public DateTime? EstimatedDeliveryTime { get; set; }
}
