using Microsoft.EntityFrameworkCore;
using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;
using TravelMgt.Infrastructure.Persistence;

namespace TravelMgt.Infrastructure.Repositories;

public sealed class HotelRepository : IHotelRepository
{
    private readonly TravelMgtDbContext _dbContext;

    public HotelRepository(TravelMgtDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Hotel>> SearchAsync(
        string? location,
        double? minRating,
        decimal? maxPrice,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Hotels.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(hotel => hotel.Location == location);
        }

        if (minRating.HasValue)
        {
            query = query.Where(hotel => hotel.Rating >= minRating.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(hotel => hotel.Price <= maxPrice.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Hotels.AsNoTracking().FirstOrDefaultAsync(hotel => hotel.Id == id, cancellationToken);
    }
}
