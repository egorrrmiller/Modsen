using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modsen.Auth.Interfaces;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;

namespace Modsen.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthRepository(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string?> GetUserToken(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUser(email, password, cancellationToken);
        if (user == null)
            return null;

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email)
        };

        var signingKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty));

        var jwt = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.Add(TimeSpan.FromHours(1)),
            notBefore: DateTime.Now,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<UserDto> RegisterUser(string email, string password, CancellationToken cancellationToken)
    {
        var result = await _userRepository.RegisterUser(new UserDto(email, password), cancellationToken);

        return new UserDto(result.Email, result.Password);
    }
}