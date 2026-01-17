namespace TravelMgt.Application.DTOs;

public sealed record HotelDto(
    Guid Id,
    string Name,
    string Location,
    double Rating,
    decimal Price,
    bool Availability);
