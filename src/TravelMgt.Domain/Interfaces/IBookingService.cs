using TravelMgt.Domain.Entities;

namespace TravelMgt.Domain.Interfaces;

public interface IBookingService
{
    Task<Booking> BookFlightAsync(Guid userId, Guid flightId, CancellationToken cancellationToken = default);
    Task<Booking> BookHotelAsync(Guid userId, Guid hotelId, CancellationToken cancellationToken = default);
    Task CancelBookingAsync(Guid bookingId, CancellationToken cancellationToken = default);
}
