namespace TravelMgt.Application.DTOs;

public sealed record FlightDto(
    Guid Id,
    string Origin,
    string Destination,
    DateTimeOffset DepartureTime,
    DateTimeOffset ArrivalTime,
    decimal Price,
    string Status);
