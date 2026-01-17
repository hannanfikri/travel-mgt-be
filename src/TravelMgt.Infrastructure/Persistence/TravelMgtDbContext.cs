using Microsoft.EntityFrameworkCore;
using TravelMgt.Domain.Entities;

namespace TravelMgt.Infrastructure.Persistence;

public sealed class TravelMgtDbContext : DbContext
{
    public TravelMgtDbContext(DbContextOptions<TravelMgtDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.FullName).HasMaxLength(200).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(200).IsRequired();
            entity.Property(user => user.PasswordHash).HasMaxLength(500).IsRequired();
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(flight => flight.Id);
            entity.Property(flight => flight.Origin).HasMaxLength(100).IsRequired();
            entity.Property(flight => flight.Destination).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(hotel => hotel.Id);
            entity.Property(hotel => hotel.Name).HasMaxLength(200).IsRequired();
            entity.Property(hotel => hotel.Location).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(booking => booking.Id);
            entity.Property(booking => booking.Status).IsRequired();
            entity.Property(booking => booking.TripType).IsRequired();
            entity.HasIndex(booking => booking.UserId);
        });
    }
}
