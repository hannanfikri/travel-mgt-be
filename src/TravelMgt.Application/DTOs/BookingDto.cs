namespace TravelMgt.Application.DTOs;

public sealed record BookingDto(
    Guid Id,
    Guid UserId,
    string TripType,
    Guid ItemId,
    string Status,
    DateTimeOffset BookingDate);
