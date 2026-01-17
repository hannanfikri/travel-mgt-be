using FluentValidation;
using TravelMgt.Application.DTOs;

namespace TravelMgt.Application.Validators;

public sealed class BookHotelRequestValidator : AbstractValidator<BookHotelRequest>
{
    public BookHotelRequestValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.HotelId).NotEmpty();
    }
}
