using Microsoft.AspNetCore.Mvc;
using DigitalBankingAPI.Models;
using DigitalBankingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace DigitalBankingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountsController : ControllerBase
{

    private readonly AppDbContext _context;
    public AccountsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccounts(int id)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            a => a.Id == id);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        var account = new Account(request.UserId, request.Balance);
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return StatusCode(201, account);

    }
    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(int id, AccountAmountRequest request)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        if (account == null)
        {
            return NotFound();
        }
        bool result = account.Deposit(request.Amount);
        if (!result)
        {
            return BadRequest("Deposit amount must be greater than zero,");
        }
        await _context.SaveChangesAsync();

        return Ok(result);
    }
    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(int id, AccountAmountRequest request)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        if(account == null)
        {
            return NotFound();
        }
        try
        {
            account.Withdraw(request.Amount);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        await _context.SaveChangesAsync();
        return Ok(account);

    }

    [HttpPost("{id}/transfer")]
    public async Task<IActionResult> Transfer(int id, TransferRequest request)
    {
        
        if (id == request.ToAccountId)
            return BadRequest("Cannot transfer to the same account.");

        var sender = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id);

        if (sender == null)
            return NotFound("Sender account not found.");

        
        var receiver = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.ToAccountId);

        if (receiver == null)
            return NotFound("Receiver account not found.");

       
        await using var transaction =
            await _context.Database.BeginTransactionAsync();
        try
        {
            sender.Withdraw(request.Amount);

            bool result = receiver.Deposit(request.Amount);
            if (!result)
            {
                await transaction.RollbackAsync();
                return BadRequest("Transfer failed.");
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                Message = "Transfer successful.",
                SenderBalance = sender.Balance,
                ReceiverBalance = receiver.Balance
            });
        }
        catch (ArgumentException ex)
        {
            await transaction.RollbackAsync();
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await transaction.RollbackAsync();
            return BadRequest(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            return Conflict("Transfer failed becuase an account was modified by another transaction. Please try again.");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

}

