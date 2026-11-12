using Microsoft.AspNetCore.Mvc;
using DigitalBankingAPI.Models;
using DigitalBankingAPI.Data;
using Microsoft.EntityFrameworkCore;

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
                a=> a.Id==id);
            if (account == null )
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
            return StatusCode(201,account);
            
        }

    }

