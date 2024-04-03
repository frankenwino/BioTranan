using BioTranan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BioTranan.Core.Database;

public static class SeedData
{
    public static void Initialise(IServiceProvider serviceProvider)
    {
        using (var context = new BioTrananDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BioTrananDbContext>>()))
        {
            if (context.Films.Any())
            {
                return; // DB has been seeded
            }

            context.Films.AddRange(
                new Film
                {
                    Title = "Flash Gordon",
                    ImdbId = "tt0080745",
                    RuntimeMinutes = 90,
                    ScreeningsAllowed = 5
                },
               new Film
               {
                   Title = "Scarface",
                   ImdbId = "tt0086250",
                   RuntimeMinutes = 120,
                   ScreeningsAllowed = 5
               },
               new Film
               {
                   Title = "The Beyond",
                   ImdbId = "tt0082307",
                   RuntimeMinutes = 85,
                   ScreeningsAllowed = 5
               }
            );

            if (context.Auditoriums.Any())
            {
                return; // DB has been seeded
            }

            context.Auditoriums.AddRange(
                new Auditorium
                {
                    Name = "The Grindhouse",
                    Seats = 25
                },
               new Auditorium
               {
                   Name = "The Avon",
                   Seats = 17
               });

            context.SaveChanges();
        }
    }
}