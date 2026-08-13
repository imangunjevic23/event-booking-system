using event_booking_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace event_booking_system.Services.Filters
{
    public class EventFilterService
    {
        public string? EventTypeFilter { get; set; }
        public string SortOrder { get; set; } = "Earliest";

        public IEnumerable<Event> ApplyFilterAndSort(IEnumerable<Event> events, DateTime now)
        {
            var query = events.AsEnumerable();

            if (!string.IsNullOrEmpty(EventTypeFilter) && Enum.TryParse<EventType>(EventTypeFilter, out var type))
            {
                query = query.Where(ev => ev.EventType == type);
            }

            if (SortOrder == "Earliest")
            {
                query = query
                    .Select(ev => new
                    {
                        Event = ev,
                        IsOngoing = IsEventOngoing(ev, now),
                        StartDateTime = ev.StartDate.ToDateTime(ev.StartTime)
                    })
                    .OrderByDescending(x => x.IsOngoing)
                    .ThenBy(x => x.StartDateTime)
                    .Select(x => x.Event);
            }
            else
            {
                query = query.OrderByDescending(ev => ev.StartDate.ToDateTime(ev.StartTime));
            }

            return query.ToList();
        }

        private bool IsEventOngoing(Event ev, DateTime now)
        {
            var start = ev.StartDate.ToDateTime(ev.StartTime);
            var end = ev.EndDate.ToDateTime(ev.EndTime);
            return now >= start && now <= end;
        }
    }
}