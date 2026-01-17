using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace TravelMgt.Tests.Integration;

public sealed class FlightsEndpointTests : IClassFixture<TravelMgtApiFactory>
{
    private readonly TravelMgtApiFactory _factory;

    public FlightsEndpointTests(TravelMgtApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetFlights_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/flights");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}

public sealed class TravelMgtApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DatabaseProvider", "InMemory");

        builder.ConfigureAppConfiguration(config =>
        {
            var settings = new Dictionary<string, string?>
            {
                ["ConnectionStrings:Default"] = "Host=localhost;Database=travelmgt;Username=postgres;Password=postgres",
                ["Jwt:Issuer"] = "TravelMgt",
                ["Jwt:Audience"] = "TravelMgt.Api",
                ["Jwt:SigningKey"] = "TEST_SIGNING_KEY_1234567890",
                ["Jwt:ExpiryMinutes"] = "60"
            };

            config.AddInMemoryCollection(settings);
        });
    }
}
