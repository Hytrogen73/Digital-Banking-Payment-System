using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Models;

public class TransferRequest
{
    [Range(1, int.MaxValue)]
    public int ToAccountId { get; set; }
    [Range(typeof(decimal), "0.001", "99999999999999.99")]
    public decimal Amount { get; set; }
}
