using DigitalBankingAPI.Data;
using DigitalBankingAPI.Models;
using DigitalBankingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly AppDbContext _context;

    public AuthController(AuthService authService, AppDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("AuthController and AuthService DI working!");
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        bool usernameExits = await _context.Users
            .AnyAsync(u => u.Username == request.Username);
        if (usernameExits)
            return Conflict("Username already Exited");
        var user = new User(request.Username);

        _authService.SetPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return StatusCode(201, new
        {
            Message = "Register Successful",
            user.Id,
            user.Username
        });

    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(a => a.Username == request.Username);
        if (user == null)
            return Unauthorized("Invalid username or password.");
        bool isPasswordValid = _authService.VerifyPassword(user, request.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid username or password.");
        var token = _authService.GenerateJwtToken(user);

        return Ok(new
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = 3600
        });

    }
}
