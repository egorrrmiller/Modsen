using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Database.Context;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly ModsenContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ModsenContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> GetUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var user = await _context.Users.FirstOrDefaultAsync(
            user => user.Email.Equals(userDto.Email) && user.Password.Equals(userDto.Password), cancellationToken);

        return user == null ? null : new UserDto(user.Email, user.Password);
    }

    public async Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var user = await _context.Users.AddAsync(new UserModel
        {
            Email = userDto.Email,
            Password = userDto.Password
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Зарегистрирован новый пользователь с email {user.Entity.Email}");

        return user.Entity;
    }
}