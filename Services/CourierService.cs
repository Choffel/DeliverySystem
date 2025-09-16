using DeliverySystem.Data;
using DeliverySystem.Models;

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
}
