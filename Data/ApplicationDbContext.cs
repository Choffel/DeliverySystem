using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Customer> Customers { get; set; }
}