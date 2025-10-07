using System;
using DeliverySystem.Enums;

namespace DeliverySystem.DTOs;

public class UpdateOrderDto
{
    public DeliveryStatus Status { get; set; }
    public Guid? CourierId { get; set; }  
    public string Product { get; set; }
    public string DeliveryAddress { get; set; }
}
