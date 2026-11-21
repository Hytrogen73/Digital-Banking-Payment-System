namespace DigitalBankingAPI.DTOs.Responses;

public class TransferResultDto
{
    public decimal SenderBalance { get; set; }

    public decimal ReceiverBalance { get; set; }
}