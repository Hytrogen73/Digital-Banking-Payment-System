using DigitalBankingAPI.Models;

namespace DigitalBankingAPI.Services.Interfaces;

public interface IAuthService
{
    bool VerifyPassword(User user, string password);

    void SetPassword(User user, string password);

    string GenerateJwtToken(User user);

    string GenerateRefreshToken();

    string HashRefreshToken(string token);
}