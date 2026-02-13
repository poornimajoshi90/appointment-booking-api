namespace AppointmentBookingAPI.Models
{
    public class IdempotencyKey
    {
        public int Id { get; set; }

        public string Key { get; set; } = null!;

        public string RequestHash { get; set; } = null!;

        public string Response { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
