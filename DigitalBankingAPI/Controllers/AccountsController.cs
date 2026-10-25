using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using DigitalBankingAPI.Models;

namespace DigitalBankingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = new[]
            {
                new Account( 5, 100.00m),
                new Account( 10, 200.00m),
            };
            return Ok(accounts);
        }

    }
}
