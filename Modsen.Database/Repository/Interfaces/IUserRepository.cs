using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository.Interfaces;

public interface IUserRepository
{
    Task<UserDto?> GetUser(string email, string password);
    Task<UserModel> RegisterUser(UserDto user);
}