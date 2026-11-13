using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Models;

public class AccountAmountRequest
{
    [Range(typeof(decimal),"0.01","99999999999.99")]
    public decimal Amount { get; set; }
}
