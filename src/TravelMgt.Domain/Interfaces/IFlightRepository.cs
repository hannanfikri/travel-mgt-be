using TravelMgt.Domain.Entities;

namespace TravelMgt.Domain.Interfaces;

public interface IFlightRepository
{
    Task<IReadOnlyList<Flight>> SearchAsync(string? origin, string? destination, DateTimeOffset? departureFrom, DateTimeOffset? departureTo, CancellationToken cancellationToken = default);
    Task<Flight?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
