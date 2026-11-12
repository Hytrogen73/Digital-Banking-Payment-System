using DigitalBankingAPI.Models;
using Xunit;

namespace DigitalBankingAPI.Tests;

public class UnitTest1
{
    [Fact]
    public void Withdraw_InsufficientBalance_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 2000.00m);

        // Act
      

        // Assert
        Assert.Throws<InvalidOperationException>(
            () => account.Withdraw(2300m));
        Assert.Equal(2000.00m, account.Balance);
    }
    [Fact]
    public void Withdraw_ValidAmount_ReducesBalance()
    {
        //arrage 
        var account = new Account(1, 2000.00m);

        //Act
        bool result = account.Withdraw(300m);

        //assert

        Assert.True(result);
        Assert.Equal(1700m, account.Balance);
    }
    [Fact]
    public  void Deposit_NegativeAmount_ReturnFalse()
    {
        //Arrage (set-up)
        var account = new Account(1,190.00m);

        //Act(Operation)

        bool result = account.Deposit(-50.00m);

        //Assert (Our Prediction)

        Assert.False(result);
        Assert.Equal(190.00m , account.Balance);
    }
    [Fact]

    public void InvalidUserId_ThrowsExceptions()
    {

        int id = 0;
        string username = "Julia";


        Assert.Throws<ArgumentException>(
           () => new User(id, username ) );
    }
    [Fact]

    public void InvalidUsername_ThrowsExceptions()
    {
        int id = 1;
        string username = "";

        Assert.Throws<ArgumentException>(() => new User(id, username ) );
    }
    [Fact]
    public void Deposit_ValidAmount_IncreasesBalance()
    {
        // Arrange
        var account = new Account(1, 500m);

        // Act
        bool result = account.Deposit(100m);

        // Assert
        Assert.True(result);
        Assert.Equal(600m, account.Balance);
    }
    [Fact]
    public void Deposit_ZeroAmount_ReturnFalse()
    {
        // Arrange
        var account = new Account(1, 500m);

        // Act
        bool result = account.Deposit(0m);

        // Assert
        Assert.False(result);
        Assert.Equal(500m, account.Balance);
    }

}
