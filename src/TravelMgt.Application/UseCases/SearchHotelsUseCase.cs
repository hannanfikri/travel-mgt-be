using TravelMgt.Application.DTOs;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.UseCases;

public sealed class SearchHotelsUseCase
{
    private readonly IHotelRepository _hotelRepository;

    public SearchHotelsUseCase(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<IReadOnlyList<HotelDto>> HandleAsync(
        string? location,
        double? minRating,
        decimal? maxPrice,
        CancellationToken cancellationToken = default)
    {
        var hotels = await _hotelRepository.SearchAsync(location, minRating, maxPrice, cancellationToken);

        return hotels
            .Select(hotel => new HotelDto(
                hotel.Id,
                hotel.Name,
                hotel.Location,
                hotel.Rating,
                hotel.Price,
                hotel.Availability))
            .ToList();
    }
}
