using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modsen.Auth.Interfaces;
using Modsen.Domain.Models;

namespace Modsen.Auth;

public class GenerateToken : IGenerateToken
{
    private readonly JwtOptionsModel _jwtOptions;

    public GenerateToken(IOptions<JwtOptionsModel> jwtOptions) => _jwtOptions = jwtOptions.Value;

    public Task<string> GenerateTokenAsync(List<Claim> claims)
    {
        var signingKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var jwt = new JwtSecurityToken(_jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.Add(TimeSpan.FromHours(1)),
            notBefore: DateTime.Now,
            signingCredentials: new(signingKey, SecurityAlgorithms.HmacSha256));

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}