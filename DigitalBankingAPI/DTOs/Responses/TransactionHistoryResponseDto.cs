namespace DigitalBankingAPI.DTOs.Responses;

public class TransactionHistoryResponseDto
{
    public int AccountId { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPages { get; set; }

    public List<TransactionResponseDto> Transactions { get; set; }
        = new();
}