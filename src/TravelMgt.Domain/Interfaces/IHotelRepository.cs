using TravelMgt.Domain.Entities;

namespace TravelMgt.Domain.Interfaces;

public interface IHotelRepository
{
    Task<IReadOnlyList<Hotel>> SearchAsync(string? location, double? minRating, decimal? maxPrice, CancellationToken cancellationToken = default);
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
