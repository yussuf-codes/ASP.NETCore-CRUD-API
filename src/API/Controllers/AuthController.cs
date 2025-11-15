using System.Threading.Tasks;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
using API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost(Endpoints.Auth.Register)]
    public async Task<IActionResult> RegisterAsync(UserRequest request)
    {
        try
        {
            await _authService.RegisterAsync(request);
            return NoContent();
        }
        catch (ConflictException)
        {
            return Conflict("Username already taken.");
        }
    }

    [HttpPost(Endpoints.Auth.Login)]
    public async Task<IActionResult> LoginAsync(UserRequest request)
    {
        try
        {
            LoginResponse response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (System.Exception)
        {
            return Unauthorized("Incorrect username or password.");
        }
    }
}
