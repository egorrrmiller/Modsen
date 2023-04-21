using System.Security.Claims;

namespace Modsen.Auth.Interfaces;

public interface IGenerateToken
{
    Task<string> GenerateTokenAsync(List<Claim> claims);
}