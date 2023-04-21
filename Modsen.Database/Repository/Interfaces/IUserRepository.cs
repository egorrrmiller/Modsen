using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository.Interfaces;

public interface IUserRepository
{
    Task<UserDto?> GetUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
}