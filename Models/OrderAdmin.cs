using DeliverySystem.Enums;

namespace DeliverySystem.Models;

public class OrderAdmin
{
    public Guid OrderId { get; set; }
    
    public Guid CourierId { get; set; }
    
    public DeliveryStatus DeliveryStatus { get; set; }
}