using Microsoft.EntityFrameworkCore;
using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;
using TravelMgt.Infrastructure.Persistence;

namespace TravelMgt.Infrastructure.Repositories;

public sealed class FlightRepository : IFlightRepository
{
    private readonly TravelMgtDbContext _dbContext;

    public FlightRepository(TravelMgtDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Flight>> SearchAsync(
        string? origin,
        string? destination,
        DateTimeOffset? departureFrom,
        DateTimeOffset? departureTo,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Flights.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(origin))
        {
            query = query.Where(flight => flight.Origin == origin);
        }

        if (!string.IsNullOrWhiteSpace(destination))
        {
            query = query.Where(flight => flight.Destination == destination);
        }

        if (departureFrom.HasValue)
        {
            query = query.Where(flight => flight.DepartureTime >= departureFrom.Value);
        }

        if (departureTo.HasValue)
        {
            query = query.Where(flight => flight.DepartureTime <= departureTo.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Flight?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Flights.AsNoTracking().FirstOrDefaultAsync(flight => flight.Id == id, cancellationToken);
    }
}
