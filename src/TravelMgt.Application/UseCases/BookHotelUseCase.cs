using TravelMgt.Application.DTOs;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.UseCases;

public sealed class BookHotelUseCase
{
    private readonly IBookingService _bookingService;

    public BookHotelUseCase(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> HandleAsync(BookHotelRequest request, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingService.BookHotelAsync(request.UserId, request.HotelId, cancellationToken);

        return new BookingDto(
            booking.Id,
            booking.UserId,
            booking.TripType.ToString(),
            booking.ItemId,
            booking.Status.ToString(),
            booking.BookingDate);
    }
}
