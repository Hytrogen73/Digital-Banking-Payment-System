namespace DigitalBankingAPI.Models;

public class CreateAccountRequest
{
    public int UserId { get; set; }
    public decimal Balance { get; set; }
}
