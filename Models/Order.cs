namespace DeliverySystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        
        public int CourierId { get; set; }
        
        public int AddressId { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}

