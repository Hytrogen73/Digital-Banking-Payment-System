using Microsoft.AspNetCore.Identity;
using DigitalBankingAPI.Models;
namespace DigitalBankingAPI.Services
{
    public class AuthService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();
        public bool VerifiyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
                );

            return result == PasswordVerificationResult.Success;
        }
        public void SetPassword(User user, string password)
        {
            string hash = _passwordHasher.HashPassword(user, password);
            user.SetPasswordHash(hash);
        }
    }
}
