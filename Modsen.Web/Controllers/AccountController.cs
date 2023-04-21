using Microsoft.AspNetCore.Mvc;
using Modsen.Auth.Interfaces;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;

namespace Modsen.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;

    public AccountController(IAuthRepository authRepository, IUserRepository userRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
    }

    [HttpGet("getToken")]
    public async Task<IActionResult> GetToken([FromQuery]UserDto userDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var token = await _authRepository.GetUserTokenAsync(userDto, cancellationToken);
        if (token == null)
            return NotFound("Пользователь не найден.");

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserDto userDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var user = await _userRepository.RegisterUserAsync(userDto, cancellationToken);

        return Ok(user);
    }
}