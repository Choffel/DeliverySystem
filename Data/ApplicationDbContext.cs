using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Courier> Couriers { get; set; }
}