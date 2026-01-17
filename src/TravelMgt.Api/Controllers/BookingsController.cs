using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMgt.Application.DTOs;
using TravelMgt.Application.UseCases;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Api.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize(Roles = "User,Admin")]
public sealed class BookingsController : ControllerBase
{
    private readonly BookFlightUseCase _bookFlightUseCase;
    private readonly BookHotelUseCase _bookHotelUseCase;
    private readonly CancelBookingUseCase _cancelBookingUseCase;
    private readonly IBookingRepository _bookingRepository;
    private readonly IValidator<BookFlightRequest> _bookFlightValidator;
    private readonly IValidator<BookHotelRequest> _bookHotelValidator;

    public BookingsController(
        BookFlightUseCase bookFlightUseCase,
        BookHotelUseCase bookHotelUseCase,
        CancelBookingUseCase cancelBookingUseCase,
        IBookingRepository bookingRepository,
        IValidator<BookFlightRequest> bookFlightValidator,
        IValidator<BookHotelRequest> bookHotelValidator)
    {
        _bookFlightUseCase = bookFlightUseCase;
        _bookHotelUseCase = bookHotelUseCase;
        _cancelBookingUseCase = cancelBookingUseCase;
        _bookingRepository = bookingRepository;
        _bookFlightValidator = bookFlightValidator;
        _bookHotelValidator = bookHotelValidator;
    }

    [HttpPost("flights")]
    public async Task<ActionResult<BookingDto>> BookFlight(BookFlightRequest request, CancellationToken cancellationToken)
    {
        var validation = await _bookFlightValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        var booking = await _bookFlightUseCase.HandleAsync(request, cancellationToken);
        return Ok(booking);
    }

    [HttpPost("hotels")]
    public async Task<ActionResult<BookingDto>> BookHotel(BookHotelRequest request, CancellationToken cancellationToken)
    {
        var validation = await _bookHotelValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        var booking = await _bookHotelUseCase.HandleAsync(request, cancellationToken);
        return Ok(booking);
    }

    [HttpDelete("{bookingId:guid}")]
    public async Task<IActionResult> Cancel(Guid bookingId, CancellationToken cancellationToken)
    {
        await _cancelBookingUseCase.HandleAsync(new CancelBookingRequest(bookingId), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetByUser([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId, cancellationToken);
        var results = bookings
            .Select(booking => new BookingDto(
                booking.Id,
                booking.UserId,
                booking.TripType.ToString(),
                booking.ItemId,
                booking.Status.ToString(),
                booking.BookingDate))
            .ToList();

        return Ok(results);
    }
}
