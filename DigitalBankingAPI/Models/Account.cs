namespace DigitalBankingAPI.Models;

public class Account
{
    public int Id { get; private set; }
    public decimal Balance { get; private set; }

    public Account(int id, decimal initialBalance)
    {
        if (id == 0 || id <0)
        {
            throw new ArgumentException("Account ID must be positive.");
        }
        if (initialBalance < 0)
        {
            throw new ArgumentException("Initial balance cannot be negative.");
        }
        Id = id;
        Balance = initialBalance;
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
            return false;
            throw new ArgumentException("Amount to withdraw must be positive and greater than 0.");
        }
        if(amount > Balance)
        {   
            return false;
            throw new ArgumentException("Insufficient Balance.");
            
        }
        Balance -= amount;
        return true;


    }

}
