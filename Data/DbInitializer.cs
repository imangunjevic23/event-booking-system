using event_booking_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace event_booking_system.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            const string seedEmail = "seed@example.com";
            var seedUser = await userManager.FindByEmailAsync(seedEmail);

            if (seedUser == null)
            {
                seedUser = new AppUser
                {
                    UserName = seedEmail,
                    Email = seedEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(seedUser, "Seed123!@#");
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create seed user: {errors}");
                }

                seedUser = await userManager.FindByEmailAsync(seedEmail);
            }

            if (await context.Events.AnyAsync())
                return;

            var events = new Event[]
            {
                new Event
                {
                    Title = "Sarajevo Jazz Fest",
                    Description = "Annual jazz festival in the heart of Sarajevo.",
                    Capacity = 200,
                    Latitude = 43.8563,
                    Longitude = 18.4131,
                    Address = "Trg Oslobođenja 1",
                    City = "Sarajevo",
                    EventType = EventType.Concert,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(12)),
                    StartTime = new TimeOnly(20, 0),
                    EndTime = new TimeOnly(23, 0),
                    CreatorId = seedUser.Id,
                    EmojiOrImageUrl = "🎷"
                },
                new Event
                {
                    Title = "Open Air Hangout",
                    Description = "Meet new people in the park.",
                    Capacity = 50,
                    Latitude = 43.8514,
                    Longitude = 18.3860,
                    Address = "Park prirode",
                    City = "Sarajevo",
                    EventType = EventType.Hangout,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    CreatorId = seedUser.Id,
                    EmojiOrImageUrl = "🧺"
                },
                new Event
                {
                    Title = "Rock Concert at Dom Mladih",
                    Description = "Local bands playing live.",
                    Capacity = 300,
                    Latitude = 43.8623,
                    Longitude = 18.3922,
                    Address = "Mladih 12",
                    City = "Sarajevo",
                    EventType = EventType.Concert,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                    StartTime = new TimeOnly(21, 0),
                    EndTime = new TimeOnly(0, 0),
                    CreatorId = seedUser.Id,
                    EmojiOrImageUrl = "🎸"
                },
                new Event
                {
                    Title = "Board Game Night",
                    Description = "Play board games with fellow enthusiasts.",
                    Capacity = 30,
                    Latitude = 43.8475,
                    Longitude = 18.3570,
                    Address = "Zmaja od Bosne 4",
                    City = "Sarajevo",
                    EventType = EventType.Hangout,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                    StartTime = new TimeOnly(18, 0),
                    EndTime = new TimeOnly(22, 0),
                    CreatorId = seedUser.Id,
                    EmojiOrImageUrl = "🎲"
                },
                new Event
                {
                    Title = "Sarajevo Film Screening",
                    Description = "Outdoor movie night.",
                    Capacity = 150,
                    Latitude = 43.8560,
                    Longitude = 18.4290,
                    Address = "Vilsonovo šetalište",
                    City = "Sarajevo",
                    EventType = EventType.Hangout,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
                    StartTime = new TimeOnly(20, 30),
                    EndTime = new TimeOnly(23, 0),
                    CreatorId = seedUser.Id,
                    EmojiOrImageUrl = "🎬"
                }
            };

            context.Events.AddRange(events);
            await context.SaveChangesAsync();
        }
    }
}