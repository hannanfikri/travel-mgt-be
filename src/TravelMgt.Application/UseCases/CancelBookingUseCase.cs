using TravelMgt.Application.DTOs;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.UseCases;

public sealed class CancelBookingUseCase
{
    private readonly IBookingService _bookingService;

    public CancelBookingUseCase(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task HandleAsync(CancelBookingRequest request, CancellationToken cancellationToken = default)
    {
        await _bookingService.CancelBookingAsync(request.BookingId, cancellationToken);
    }
}
