using DeliverySystem.Enums;

namespace DeliverySystem.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        public string OrderNumber { get; set; }
        
        public Guid? CourierId { get; set; }
        
        public string DeliveryAddress { get; set; }
        
        public string Product { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public DeliveryStatus Status { get; set; } 
        
        public Customer User { get; set; }
        
    }
}
