using DigitalBankingAPI.Models;
using System.ComponentModel.DataAnnotations;
using DigitalBankingAPI.Data;
using Microsoft.EntityFrameworkCore;
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
    [Fact]
    public void AccountAmountRequest_NegativeAmount_FailsValidation()
    {
        var request = new AccountAmountRequest
        {
            Amount = 0m
        };

        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(request);

        bool isValid = Validator.TryValidateObject(
            request,
            context,
            validationResults,
            validateAllProperties: true
        );

        Assert.False(isValid);
    }

    [Fact]
    public async Task RowVersion_ConcurrentUpdates_ThrowsConcurrencyException()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Server=localhost;Database=DigitalBankingDB;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        int userId = 0;
        int accountId = 0;

        try
        {
            // Arrange: Create temporary user and account
            await using (var setupContext = new AppDbContext(options))
            {
                var user = new User($"ConcurrencyTest_{Guid.NewGuid()}");
                setupContext.Users.Add(user);
                await setupContext.SaveChangesAsync();

                userId = user.Id;

                var account = new Account(userId, 1000m);
                setupContext.Accounts.Add(account);
                await setupContext.SaveChangesAsync();

                accountId = account.Id;
            }

            // Two contexts load the same original account version
            await using var contextA = new AppDbContext(options);
            await using var contextB = new AppDbContext(options);

            var accountA = await contextA.Accounts.FindAsync(accountId);
            var accountB = await contextB.Accounts.FindAsync(accountId);

            Assert.NotNull(accountA);
            Assert.NotNull(accountB);

            // Context A saves first
            accountA.Withdraw(100m);
            await contextA.SaveChangesAsync();

            // Context B tries to save its stale version
            accountB.Withdraw(200m);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(
                async () => await contextB.SaveChangesAsync()
            );

            // Verify that only Context A's update was saved
            await using var verifyContext = new AppDbContext(options);

            var finalAccount = await verifyContext.Accounts.FindAsync(accountId);

            Assert.NotNull(finalAccount);
            Assert.Equal(900m, finalAccount.Balance);
        }
        finally
        {
            // Clean up temporary database records
            await using var cleanupContext = new AppDbContext(options);

            var account = await cleanupContext.Accounts.FindAsync(accountId);

            if (account != null)
                cleanupContext.Accounts.Remove(account);

            var user = await cleanupContext.Users.FindAsync(userId);

            if (user != null)
                cleanupContext.Users.Remove(user);

            await cleanupContext.SaveChangesAsync();
        }
    }


}
