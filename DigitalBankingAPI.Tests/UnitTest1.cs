using DigitalBankingAPI.Models;
using System.Reflection.Emit;
using Xunit;

namespace DigitalBankingAPI.Tests;

public class UnitTest1
{
    [Fact]
    public void Withdraw_InsufficientBalance_ReturnFalse()
    {
        // Arrange
        var account = new Account(15, 200.00m);

        // Act
        bool result = account.Withdraw(250.00m);

        // Assert
        Assert.False(result);
        Assert.Equal(200.00m, account.Balance);
    }
    [Fact]
    public void Withdraw_ValidAmount_ReducesBalance()
    {
        //arrage 
        var account = new Account(35, 2000.00m);

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
        var account = new Account(20,190.00m);

        //Act(Operation)

        bool result = account.Deposit(-50.00m);

        //Assert (Our Prediction)

        Assert.False(result);
        Assert.Equal(190.00m , account.Balance);
    }
    
}
