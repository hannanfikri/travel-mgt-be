using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.Services;

public sealed class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IHotelRepository _hotelRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IFlightRepository flightRepository,
        IHotelRepository hotelRepository)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _hotelRepository = hotelRepository;
    }

    public async Task<Booking> BookFlightAsync(Guid userId, Guid flightId, CancellationToken cancellationToken = default)
    {
        var flight = await _flightRepository.GetByIdAsync(flightId, cancellationToken);
        if (flight is null || flight.Status == FlightStatus.Cancelled)
        {
            throw new InvalidOperationException("Flight is not available.");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TripType = TripType.Flight,
            ItemId = flightId,
            Status = BookingStatus.Confirmed,
            BookingDate = DateTimeOffset.UtcNow
        };

        await _bookingRepository.AddAsync(booking, cancellationToken);
        return booking;
    }

    public async Task<Booking> BookHotelAsync(Guid userId, Guid hotelId, CancellationToken cancellationToken = default)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId, cancellationToken);
        if (hotel is null || !hotel.Availability)
        {
            throw new InvalidOperationException("Hotel is not available.");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TripType = TripType.Hotel,
            ItemId = hotelId,
            Status = BookingStatus.Confirmed,
            BookingDate = DateTimeOffset.UtcNow
        };

        await _bookingRepository.AddAsync(booking, cancellationToken);
        return booking;
    }

    public async Task CancelBookingAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        if (booking is null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.UpdateAsync(booking, cancellationToken);
    }
}
