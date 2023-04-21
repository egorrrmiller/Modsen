using Microsoft.EntityFrameworkCore;
using Modsen.Database.Context;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly ModsenContext _context;

    public UserRepository(ModsenContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> GetUser(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            user => user.Email.Equals(email) && user.Password.Equals(password));

        return user == null ? null : new UserDto(user.Email, user.Password);
    }

    public async Task<UserModel> RegisterUser(UserDto userDto)
    {
        var user = await _context.Users.AddAsync(new UserModel
        {
            Email = userDto.Email,
            Password = userDto.Password
        });

        await _context.SaveChangesAsync();

        return user.Entity;
    }
}