namespace TravelMgt.Domain.Entities;

public sealed class Flight
{
    public Guid Id { get; init; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTimeOffset DepartureTime { get; set; }
    public DateTimeOffset ArrivalTime { get; set; }
    public decimal Price { get; set; }
    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;
}

public enum FlightStatus
{
    Scheduled = 0,
    Delayed = 1,
    Cancelled = 2
}
