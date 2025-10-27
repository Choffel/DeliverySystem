using Microsoft.AspNetCore.Identity;

namespace DeliverySystem.Models;

public class Customer : IdentityUser
{
    public List<Order> Orders { get; set; } = new();
}