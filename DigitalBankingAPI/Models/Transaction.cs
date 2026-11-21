namespace DigitalBankingAPI.Models;

public class Transaction
{
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid? TransferReferenceId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Transaction()
    {

    }
    public Transaction(int accountId, decimal amount, TransactionType type, Guid? transferReferenceId = null)
    {
        if (accountId <= 0)
            throw new ArgumentException("Account ID must be POSITIVE. ");
        if (amount <= 0) throw new ArgumentException("Transaction Amount must be Positive");
        if (!Enum.IsDefined(typeof(TransactionType), type)) throw new ArgumentException("Invalid transaction Type");

        bool isTransfer =
            type == TransactionType.TransferSent ||
            type == TransactionType.TransferReceived;

        if (isTransfer && (!transferReferenceId.HasValue || transferReferenceId.Value == Guid.Empty))
            throw new ArgumentException("Transfer reference is required.");
        if (!isTransfer && transferReferenceId.HasValue)
            throw new ArgumentException("Non-transfer transactions cannot have a transfer reference.");
        AccountId = accountId;
        Amount = amount;
        Type = type;
        TransferReferenceId = transferReferenceId;
        CreatedAt = DateTime.UtcNow;
    
    }
} 
