using TravelMgt.Application.DTOs;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.UseCases;

public sealed class SearchFlightsUseCase
{
    private readonly IFlightRepository _flightRepository;

    public SearchFlightsUseCase(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<IReadOnlyList<FlightDto>> HandleAsync(
        string? origin,
        string? destination,
        DateTimeOffset? departureFrom,
        DateTimeOffset? departureTo,
        CancellationToken cancellationToken = default)
    {
        var flights = await _flightRepository.SearchAsync(origin, destination, departureFrom, departureTo, cancellationToken);

        return flights
            .Select(flight => new FlightDto(
                flight.Id,
                flight.Origin,
                flight.Destination,
                flight.DepartureTime,
                flight.ArrivalTime,
                flight.Price,
                flight.Status.ToString()))
            .ToList();
    }
}
