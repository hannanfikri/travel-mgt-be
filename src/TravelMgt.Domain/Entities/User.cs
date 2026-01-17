namespace TravelMgt.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public string PasswordHash { get; set; } = string.Empty;
}

public enum UserRole
{
    User = 0,
    Admin = 1
}
