using Microsoft.AspNetCore.Mvc;
using DigitalBankingAPI.Services;
using System.Reflection;

namespace DigitalBankingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase 
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService) 
    {
        _authService = authService;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("AuthController and AuthService DI working!");
    }


}
