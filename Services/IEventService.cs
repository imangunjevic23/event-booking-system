using event_booking_system.Models;

namespace event_booking_system.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task CreateAsync(Event newEvent, string creatorId);
        Task<List<Event>> GetByCreatorIdAsync(string creatorId);
        Task<bool> BookEventAsync(int eventId, string userId);
        Task<bool> CancelBookingAsync(int eventId, string userId);
        Task<List<int>> GetBookedEventIdsAsync(string userId);
        Task<Dictionary<int, int>> GetBookingCountsAsync();
        Task<List<Event>> GetBookedEventsAsync(string userId);
        Task<List<EventBooking>> GetBookingsForCreatedEventsAsync(string creatorId);
    }
}