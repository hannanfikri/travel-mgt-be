using TravelMgt.Domain.Entities;

namespace TravelMgt.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
