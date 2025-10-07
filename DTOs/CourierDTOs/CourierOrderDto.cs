using DeliverySystem.Enums;

namespace DeliverySystem.DTOs;

public class CourierOrderDto
{
    public string OrderNumber { get; set; }
    public string DeliveryAddress { get; set; }
    public DeliveryStatus Status { get; set; }
}