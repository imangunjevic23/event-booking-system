namespace event_booking_system.Models
{
    public enum EventType 
    { 
        Concert, 
        Hangout 
    }

    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        //filterable
        public EventType EventType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public string CreatorId { get; set; } = string.Empty;
        
        public AppUser Creator { get; set; } = null!;

        public string? EmojiOrImageUrl { get; set; }
        public ICollection<EventBooking> Bookings { get; set; } = new List<EventBooking>();
    }
}
