using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Models;


public class Account
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public decimal Balance { get; private set; }
    [Timestamp]
    public byte[] RowVersion { get; private set; } = null!;
    private Account()
    {

    }
    public Account( int userId, decimal balance)
    {
      
        if(userId <= 0)
        {
            throw new ArgumentException("User ID must be positive.");
        }
        if (balance < 0)
        {
            throw new ArgumentException("Initial balance cannot be negative.");
        }
      
        Balance = balance;
        UserId = userId;
    }
    public bool Deposit(decimal amount)
    {
        if(amount<=0)
        {
            return false;
        }
        Balance += amount;
        return true;
    }
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            
            throw new ArgumentException("Amount to withdraw must be positive and greater than 0.");
        }
        if(amount > Balance)
        {
         
            throw new InvalidOperationException("Insufficient Balance.");
            
        }
        Balance -= amount;
        return true;


    }

}
