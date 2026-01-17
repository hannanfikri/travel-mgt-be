using FluentValidation;
using TravelMgt.Application.DTOs;

namespace TravelMgt.Application.Validators;

public sealed class BookFlightRequestValidator : AbstractValidator<BookFlightRequest>
{
    public BookFlightRequestValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.FlightId).NotEmpty();
    }
}
