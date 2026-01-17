using System.Security.Cryptography;
using TravelMgt.Application.Interfaces;

namespace TravelMgt.Infrastructure.Auth;

public sealed class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const int Iterations = 100000;
    private const int SaltSize = 16;
    private const int KeySize = 32;

    public string Hash(string input)
    {
        using var algorithm = new Rfc2898DeriveBytes(input, SaltSize, Iterations, HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{Iterations}.{salt}.{key}";
    }

    public bool Verify(string input, string hash)
    {
        var parts = hash.Split('.', 3);
        if (parts.Length != 3)
        {
            return false;
        }

        if (!int.TryParse(parts[0], out var iterations))
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[1]);
        var expectedKey = parts[2];

        using var algorithm = new Rfc2898DeriveBytes(input, salt, iterations, HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));

        return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(expectedKey), Convert.FromBase64String(key));
    }
}
