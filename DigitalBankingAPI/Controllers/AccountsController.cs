using DigitalBankingAPI.DTOs.Requests;
using DigitalBankingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DigitalBankingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountsController : ControllerBase
{


    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccounts(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        var account = await _accountService.GetAccountAsync(id, userId);

        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        var account = await _accountService.CreateAccountAsync(
            userId,
            request.Balance
        );

        return StatusCode(201, account);
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(int id, AccountAmountRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        try
        {
            var account = await _accountService.DepositAsync(
                id, userId, request.Amount
            );

            if (account == null)
                return NotFound();

            return Ok(new
            {
                Message = "Deposit successful.",
                AccountId = account.Id,
                Amount = request.Amount,
                Balance = account.Balance
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(int id, AccountAmountRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        try
        {
            var account = await _accountService.WithdrawAsync(
                id, userId, request.Amount
            );

            if (account == null)
                return NotFound();

            return Ok(new
            {
                Message = "Withdraw successful.",
                AccountId = account.Id,
                Amount = request.Amount,
                Balance = account.Balance
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/transfer")]
    public async Task<IActionResult> Transfer(int id, TransferRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        string idempotencyKey =
            Request.Headers["Idempotency-Key"].ToString();

        if (string.IsNullOrWhiteSpace(idempotencyKey))
            return BadRequest("Idempotency-Key header is required.");

        if (idempotencyKey.Length > 100)
            return BadRequest("Idempotency-Key cannot exceed 100 characters.");

        try
        {
            var result = await _accountService.TransferAsync(
                id,
                request.ToAccountId,
                userId,
                request.Amount,
                idempotencyKey);

            return Ok(new
            {
                Message = "Transfer successful.",
                SenderBalance = result.SenderBalance,
                ReceiverBalance = result.ReceiverBalance
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(
                "Transfer failed because an account was modified by another transaction. Please try again.");
        }
    }
    [HttpGet("{id}/transactions")]
    public async Task<IActionResult> GetTransactionHistory(int id, int page = 1,int pageSize = 10)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        try
        {
            var result = await _accountService.GetTransactionHistoryAsync(
                id, userId, page, pageSize);

            if (result == null)
                return NotFound("Account not found.");

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

