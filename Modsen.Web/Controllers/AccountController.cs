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
    public async Task<IActionResult> GetToken(string email, string password)
    {
        var token = await _auth.GetUserToken(email, password);
        if (token == null)
            return NotFound("Пользователь не найден.");

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserDto userDto)
    {
        var user = await _auth.RegisterUser(userDto.Email, userDto.Password);

        return Ok(user);
    }
}