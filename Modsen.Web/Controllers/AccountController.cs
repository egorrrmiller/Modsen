using Microsoft.AspNetCore.Mvc;
using Modsen.Auth.Interfaces;
using Modsen.Domain.Dto;

namespace Modsen.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthRepository _auth;

    public AccountController(IAuthRepository auth)
    {
        _auth = auth;
    }

    [HttpGet("getToken")]
    public async Task<IActionResult> GetToken(string email, string password, CancellationToken cancellationToken)
    {
        var token = await _auth.GetUserToken(email, password, cancellationToken);
        if (token == null)
            return NotFound("Пользователь не найден.");

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _auth.RegisterUser(userDto.Email, userDto.Password, cancellationToken);

        return Ok(user);
    }
}