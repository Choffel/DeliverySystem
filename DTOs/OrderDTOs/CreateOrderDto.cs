using System;

namespace DeliverySystem.DTOs;

public class CreateOrderDto
{
    public string DeliveryAddress { get; set; }
    public string Product { get; set; }
}
