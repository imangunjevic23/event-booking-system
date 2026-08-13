namespace event_booking_system.Models
{
    public class EventBooking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    }
}