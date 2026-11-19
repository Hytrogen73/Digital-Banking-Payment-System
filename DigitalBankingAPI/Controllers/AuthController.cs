using DigitalBankingAPI.Data;
using DigitalBankingAPI.Models;
using DigitalBankingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        var refreshToken = _authService.GenerateRefreshToken();
        var refreshTokenHash = _authService.HashRefreshToken(refreshToken);
        var refreshTokenRecord = new RefreshToken
        {
            UserId = user.Id,
            TokenHash = refreshTokenHash,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };
        _context.RefreshTokens.Add(refreshTokenRecord);
        await _context.SaveChangesAsync();
        return Ok(new
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = 3600,
            RefreshToken = refreshToken
        });

    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var tokenHash = _authService.HashRefreshToken(request.RefreshToken);

        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
        if (storedToken == null)
            return Unauthorized("Invalid refresh token.");
        if (storedToken.IsRevoked || storedToken.ExpiresAt <= DateTime.UtcNow)
            return Unauthorized("Refresh token is expired or revoked.");

        var newAccessToken = _authService.GenerateJwtToken(storedToken.User);
        var newRefreshToken = _authService.GenerateRefreshToken();
        var newRefreshTokenHash = _authService.HashRefreshToken(newRefreshToken);
        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;

        var newRefreshTokenRecord = new RefreshToken
        {
            UserId = storedToken.UserId,
            TokenHash = newRefreshTokenHash,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false

        };
        _context.RefreshTokens.Add(newRefreshTokenRecord);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            AccessToken = newAccessToken,
            TokenType = "Bearer",
            ExpiresIn = 3600,
            RefreshToken = newRefreshToken
        });
    }
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(RefreshRequest request)
    {
        var tokenHash = _authService.HashRefreshToken(request.RefreshToken);
        var storedToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);

        if (storedToken == null || storedToken.IsRevoked)
            return Unauthorized("Invalid or already revoke refresh token.");

        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok("Lagout successful.");
    }
}
