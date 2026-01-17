namespace TravelMgt.Domain.Entities;

public sealed class Booking
{
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public TripType TripType { get; set; }
    public Guid ItemId { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
    public DateTimeOffset BookingDate { get; set; }
}

public enum TripType
{
    Flight = 0,
    Hotel = 1
}

public enum BookingStatus
{
    Confirmed = 0,
    Cancelled = 1
}
