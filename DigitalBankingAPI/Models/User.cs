using System.Globalization;

namespace DigitalBankingAPI.Models;

public class User
{
    public int Id { get; private set; }
    public string Username { get;private set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();
    public string PasswordHash { get; private set; } = string.Empty;
    public User (int id, string username)
    {
        Id = id;
        Username = username;
     

    }
    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    
}
