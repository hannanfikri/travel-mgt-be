using TravelMgt.Application.DTOs.Auth;

namespace TravelMgt.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
