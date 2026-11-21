namespace DigitalBankingAPI.Models;

public class IdempotencyRecord
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string IdempotencyKey { get; set; } = null!;

    public int SenderAccountId { get; set; }

    public int ReceiverAccountId { get; set; }

    public decimal Amount { get; set; }

    public decimal SenderBalance { get; set; }

    public decimal ReceiverBalance { get; set; }

    public DateTime CreatedAt { get; set; }
}