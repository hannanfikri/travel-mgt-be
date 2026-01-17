using TravelMgt.Application.DTOs;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.UseCases;

public sealed class BookFlightUseCase
{
    private readonly IBookingService _bookingService;

    public BookFlightUseCase(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> HandleAsync(BookFlightRequest request, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingService.BookFlightAsync(request.UserId, request.FlightId, cancellationToken);

        return new BookingDto(
            booking.Id,
            booking.UserId,
            booking.TripType.ToString(),
            booking.ItemId,
            booking.Status.ToString(),
            booking.BookingDate);
    }
}
