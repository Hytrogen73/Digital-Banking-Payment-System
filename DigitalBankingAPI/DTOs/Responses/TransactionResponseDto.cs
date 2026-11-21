using DigitalBankingAPI.Models;

namespace DigitalBankingAPI.DTOs.Responses;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Guid? TransferReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
}