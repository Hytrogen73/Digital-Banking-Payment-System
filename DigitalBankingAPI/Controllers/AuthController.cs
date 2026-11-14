using Microsoft.AspNetCore.Mvc;
using DigitalBankingAPI.Services;
using DigitalBankingAPI.Data;
using DigitalBankingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
}
