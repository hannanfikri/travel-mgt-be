using Microsoft.AspNetCore.Mvc;
using TravelMgt.Application.DTOs;
using TravelMgt.Application.UseCases;

namespace TravelMgt.Api.Controllers;

[ApiController]
[Route("api/hotels")]
public sealed class HotelsController : ControllerBase
{
    private readonly SearchHotelsUseCase _searchHotelsUseCase;

    public HotelsController(SearchHotelsUseCase searchHotelsUseCase)
    {
        _searchHotelsUseCase = searchHotelsUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<HotelDto>>> Search(
        [FromQuery] string? location,
        [FromQuery] double? minRating,
        [FromQuery] decimal? maxPrice,
        CancellationToken cancellationToken)
    {
        var results = await _searchHotelsUseCase.HandleAsync(location, minRating, maxPrice, cancellationToken);
        return Ok(results);
    }
}
