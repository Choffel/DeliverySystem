using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Services;

public class CourierService
{
    private readonly ApplicationDbContext _context;
    
    
    public CourierService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task AddCourierAsync(Courier courier)
    {
        _context.Add(courier);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Courier>> GetAllCouriersAsync()
    {
        return await _context.Couriers.ToListAsync();
    }
}
