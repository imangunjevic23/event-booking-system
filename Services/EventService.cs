using event_booking_system.Data;
using event_booking_system.Models;
using Microsoft.EntityFrameworkCore;

namespace event_booking_system.Services
{
    public class EventService : IEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public EventService(IDbContextFactory<ApplicationDbContext> dbFactory)
            => _dbFactory = dbFactory;

        public async Task<List<Event>> GetAllAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Events.AsNoTracking().ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Events.FindAsync(id);
        }

        public async Task CreateAsync(Event newEvent, string creatorId)
        {
            newEvent.CreatorId = creatorId;
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Events.Add(newEvent);
            await db.SaveChangesAsync();
        }

        public async Task<List<Event>> GetByCreatorIdAsync(string creatorId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Events
                .AsNoTracking()
                .Where(e => e.CreatorId == creatorId)
                .ToListAsync();
        }

        public async Task<bool> BookEventAsync(int eventId, string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var ev = await db.Events.FindAsync(eventId);
            if (ev == null) return false;

            var existing = await db.EventBookings
                .AnyAsync(b => b.EventId == eventId && b.UserId == userId);
            if (existing) return false;

            int currentBookings = await db.EventBookings.CountAsync(b => b.EventId == eventId);
            if (currentBookings >= ev.Capacity) return false;

            db.EventBookings.Add(new EventBooking
            {
                EventId = eventId,
                UserId = userId,
                BookedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int eventId, string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var booking = await db.EventBookings
                .FirstOrDefaultAsync(b => b.EventId == eventId && b.UserId == userId);
            if (booking == null) return false;

            db.EventBookings.Remove(booking);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<int>> GetBookedEventIdsAsync(string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.EventBookings
                .Where(b => b.UserId == userId)
                .Select(b => b.EventId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetBookingCountsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.EventBookings
                .GroupBy(b => b.EventId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<List<Event>> GetBookedEventsAsync(string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.EventBookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Event)
                .Select(b => b.Event)
                .ToListAsync();
        }

        public async Task<List<EventBooking>> GetBookingsForCreatedEventsAsync(string creatorId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.EventBookings
                .Where(b => b.Event.CreatorId == creatorId)
                .Include(b => b.Event)
                .Include(b => b.User)
                .ToListAsync();
        }
    }
}