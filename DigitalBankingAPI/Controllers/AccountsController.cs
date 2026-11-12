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


}

