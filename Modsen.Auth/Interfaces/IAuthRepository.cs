using Modsen.Domain.Dto;

namespace Modsen.Auth.Interfaces;

public interface IAuthRepository
{
    Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default);
}