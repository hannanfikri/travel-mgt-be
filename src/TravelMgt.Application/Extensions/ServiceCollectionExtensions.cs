using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TravelMgt.Application.DTOs;
using TravelMgt.Application.Interfaces;
using TravelMgt.Application.Services;
using TravelMgt.Application.UseCases;
using TravelMgt.Application.Validators;
using TravelMgt.Domain.Interfaces;

namespace TravelMgt.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<BookFlightUseCase>();
        services.AddScoped<BookHotelUseCase>();
        services.AddScoped<CancelBookingUseCase>();
        services.AddScoped<SearchFlightsUseCase>();
        services.AddScoped<SearchHotelsUseCase>();

        services.AddValidatorsFromAssemblyContaining<BookFlightRequestValidator>();
        services.AddScoped<IValidator<BookFlightRequest>, BookFlightRequestValidator>();
        services.AddScoped<IValidator<BookHotelRequest>, BookHotelRequestValidator>();

        return services;
    }
}
