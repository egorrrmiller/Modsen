using Modsen.Domain.Models;

namespace Modsen.Database.Repository.Interfaces;

public interface IUserRepository
{
    Task<bool> AddUser(string email, string password);
    Task<UserModel?> GetUser(string email);
}