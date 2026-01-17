using Microsoft.AspNetCore.Mvc;
using TravelMgt.Application.DTOs;
using TravelMgt.Application.UseCases;

namespace TravelMgt.Api.Controllers;

[ApiController]
[Route("api/flights")]
public sealed class FlightsController : ControllerBase
{
    private readonly SearchFlightsUseCase _searchFlightsUseCase;

    public FlightsController(SearchFlightsUseCase searchFlightsUseCase)
    {
        _searchFlightsUseCase = searchFlightsUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FlightDto>>> Search(
        [FromQuery] string? origin,
        [FromQuery] string? destination,
        [FromQuery] DateTimeOffset? departureFrom,
        [FromQuery] DateTimeOffset? departureTo,
        CancellationToken cancellationToken)
    {
        var results = await _searchFlightsUseCase.HandleAsync(origin, destination, departureFrom, departureTo, cancellationToken);
        return Ok(results);
    }
}
