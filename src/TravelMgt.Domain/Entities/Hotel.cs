namespace TravelMgt.Domain.Entities;

public sealed class Hotel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Rating { get; set; }
    public decimal Price { get; set; }
    public bool Availability { get; set; }
}
