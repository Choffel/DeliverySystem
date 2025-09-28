using System;
using DeliverySystem.Enums;

namespace DeliverySystem.DTOs;

public class OrderHistoryDto
{
    public string OrderNumber { get; set; }
    public string Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DeliveryStatus Status { get; set; }
    public bool IsDelivered => Status == DeliveryStatus.Delivered;
    public DateTime? DeliveredAt { get; set; }
}
