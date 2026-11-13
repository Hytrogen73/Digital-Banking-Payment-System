
namespace DigitalBankingAPI.Models;

public class User
{
    public int Id { get; private set; }
    public string Username { get;private set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();
    public string PasswordHash { get; private set; } = string.Empty;
    public User(int id, string username)
    {
        if (id <= 0)
        {
            throw new ArgumentException("User ID must be positive.");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be empty.");
        }

        Id = id;
        Username = username;
    }
    public User(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be empty.");
        }

        Username = username;
    }
    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    
}
