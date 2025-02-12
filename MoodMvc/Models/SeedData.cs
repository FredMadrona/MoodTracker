using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoodMvc.Models;
using MoodMvc.Data;
using System;
using System.Linq;

namespace MvcMoody.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MoodMvcContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MoodMvcContext>>()))
        {
            // Look for any mood entries.
            if (context.MoodEntry.Any()) // Corrected
            {
                return;   // DB has been seeded
            }
            context.MoodEntry.AddRange(
                new MoodEntry
                {
                    UserId = "user1",
                    Date = DateTime.Parse("2025-02-01"),
                    Mood = "Happy",
                    JournalEntry = "Had a great day today!"
                },
                new MoodEntry
                {
                    UserId = "user2",
                    Date = DateTime.Parse("2025-02-02"),
                    Mood = "Sad",
                    JournalEntry = "Feeling a bit down today."
                },
                new MoodEntry
                {
                    UserId = "user3",
                    Date = DateTime.Parse("2025-02-03"),
                    Mood = "Excited",
                    JournalEntry = "Looking forward to the weekend!"
                },
                new MoodEntry
                {
                    UserId = "user4",
                    Date = DateTime.Parse("2025-02-04"),
                    Mood = "Tired",
                    JournalEntry = "Didn't get much sleep last night."
                }
            );
            context.SaveChanges();
        }
    }
}
