using Modsen.Database.Context;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly ModsenContext _context;

    public UserRepository(ModsenContext context)
    {
        _context = context;
    }

    public async Task<bool> AddUser(string email, string password)
    {
        var user = await _context.Users.FindAsync(email);
        
        if (user == null) 
            return false;
        
        await _context.Users.AddAsync(new UserModel()
        {
            Email = email,
            Password = password
        });
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<UserModel?> GetUser(string email)
    {
        return await _context.Users.FindAsync(email);
    }
}