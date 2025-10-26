namespace DeliverySystem.DTOs.CourierDTOs
{
    public class CourierResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
