using Microsoft.EntityFrameworkCore;
using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;
using TravelMgt.Infrastructure.Persistence;

namespace TravelMgt.Infrastructure.Repositories;

public sealed class BookingRepository : IBookingRepository
{
    private readonly TravelMgtDbContext _dbContext;

    public BookingRepository(TravelMgtDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        _dbContext.Bookings.Add(booking);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .AsNoTracking()
            .Where(booking => booking.UserId == userId)
            .OrderByDescending(booking => booking.BookingDate)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        _dbContext.Bookings.Update(booking);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
