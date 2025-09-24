using DeliverySystem.Enums;

namespace DeliverySystem.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public int CustomerId { get; set; }
        
        public int CourierId { get; set; }
        
        public int AddressId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DeliveryStatus Status { get; set; } // Статус доставки
    }
}
