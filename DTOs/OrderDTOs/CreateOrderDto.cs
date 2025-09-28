using System;

namespace DeliverySystem.DTOs;

public class CreateOrderDto
{
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string DeliveryAddress { get; set; }
    public string Product { get; set; }
}
