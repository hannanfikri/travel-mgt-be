using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelMgt.Application.Interfaces;
using TravelMgt.Domain.Entities;

namespace TravelMgt.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TravelMgtDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
        else
        {
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        }

        if (!await dbContext.Users.AnyAsync(cancellationToken))
        {
            dbContext.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                FullName = "Admin User",
                Email = "admin@travelmgt.local",
                Role = UserRole.Admin,
                PasswordHash = passwordHasher.Hash("P@ssw0rd!")
            });
        }

        if (!await dbContext.Flights.AnyAsync(cancellationToken))
        {
            dbContext.Flights.AddRange(new[]
            {
                new Flight
                {
                    Id = Guid.NewGuid(),
                    Origin = "SIN",
                    Destination = "LAX",
                    DepartureTime = DateTimeOffset.UtcNow.AddDays(2),
                    ArrivalTime = DateTimeOffset.UtcNow.AddDays(2).AddHours(6),
                    Price = 799,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = Guid.NewGuid(),
                    Origin = "JFK",
                    Destination = "LHR",
                    DepartureTime = DateTimeOffset.UtcNow.AddDays(3),
                    ArrivalTime = DateTimeOffset.UtcNow.AddDays(3).AddHours(7),
                    Price = 650,
                    Status = FlightStatus.Scheduled
                }
            });
        }

        if (!await dbContext.Hotels.AnyAsync(cancellationToken))
        {
            dbContext.Hotels.AddRange(new[]
            {
                new Hotel
                {
                    Id = Guid.NewGuid(),
                    Name = "Seaside Resort",
                    Location = "Bali",
                    Rating = 4.5,
                    Price = 180,
                    Availability = true
                },
                new Hotel
                {
                    Id = Guid.NewGuid(),
                    Name = "City Central Hotel",
                    Location = "Tokyo",
                    Rating = 4.2,
                    Price = 220,
                    Availability = true
                }
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
