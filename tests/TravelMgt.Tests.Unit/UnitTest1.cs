using FluentAssertions;
using TravelMgt.Application.DTOs;
using TravelMgt.Application.UseCases;
using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Tests.Unit;

public sealed class BookFlightUseCaseTests
{
    [Fact]
    public async Task HandleAsync_ReturnsBookingDetails()
    {
        var bookingService = new StubBookingService();
        var useCase = new BookFlightUseCase(bookingService);
        var request = new BookFlightRequest(Guid.NewGuid(), Guid.NewGuid());

        var result = await useCase.HandleAsync(request);

        result.TripType.Should().Be("Flight");
        result.UserId.Should().Be(request.UserId);
        result.ItemId.Should().Be(request.FlightId);
        result.Status.Should().Be("Confirmed");
    }

    private sealed class StubBookingService : IBookingService
    {
        public Task<Booking> BookFlightAsync(Guid userId, Guid flightId, CancellationToken cancellationToken = default)
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TripType = TripType.Flight,
                ItemId = flightId,
                Status = BookingStatus.Confirmed,
                BookingDate = DateTimeOffset.UtcNow
            };

            return Task.FromResult(booking);
        }

        public Task<Booking> BookHotelAsync(Guid userId, Guid hotelId, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task CancelBookingAsync(Guid bookingId, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}
