using Modsen.Domain.Dto;

namespace Modsen.Auth.Interfaces;

public interface IAuthRepository
{
    Task<string?> GetUserToken(string email, string password, CancellationToken cancellationToken);

    Task<UserDto> RegisterUser(string email, string password, CancellationToken cancellationToken);
}