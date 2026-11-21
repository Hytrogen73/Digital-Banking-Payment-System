using DigitalBankingAPI.Data;
using DigitalBankingAPI.DTOs.Responses;
using DigitalBankingAPI.Models;
using DigitalBankingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DigitalBankingAPI.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountAsync(int accountId, int userId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(
            a => a.Id == accountId && a.UserId == userId
        );
    }
    public async Task<Account> CreateAccountAsync(int userId, decimal balance)
    {
        var account = new Account(userId, balance);

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }
    public async Task<Account?> DepositAsync( int accountId, int userId, decimal amount)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            a => a.Id == accountId && a.UserId == userId
        );

        if (account == null)
            return null;

        bool result = account.Deposit(amount);

        if (!result)
            throw new ArgumentException(
                "Deposit amount must be greater than zero."
            );

        var transaction = new Transaction(
            account.Id,
            amount,
            TransactionType.Deposit
        );

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return account;
    }
    public async Task<Account?> WithdrawAsync( int accountId, int userId, decimal amount)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            a => a.Id == accountId && a.UserId == userId
        );

        if (account == null)
            return null;

        account.Withdraw(amount);

        var transaction = new Transaction(
            account.Id,
            amount,
            TransactionType.Withdrawal
        );

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<TransferResultDto> TransferAsync(
        int senderId,
        int receiverId,
        int userId,
        decimal amount,
        string idempotencyKey)
    {
        if (senderId == receiverId)
            throw new ArgumentException("Cannot transfer to the same account.");

        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

        try
        {
         
            var existingRequest = await _context.IdempotencyRecords
                .FirstOrDefaultAsync(i =>
                    i.UserId == userId &&
                    i.IdempotencyKey == idempotencyKey);

            if (existingRequest != null)
            {
            
                if (existingRequest.SenderAccountId != senderId ||
                    existingRequest.ReceiverAccountId != receiverId ||
                    existingRequest.Amount != amount)
                {
                    throw new InvalidOperationException(
                        "Idempotency key already used with a different request.");
                }

                await transaction.CommitAsync();

               
                return new TransferResultDto
                {
                    SenderBalance = existingRequest.SenderBalance,
                    ReceiverBalance = existingRequest.ReceiverBalance
                };
            }

           
            var sender = await _context.Accounts.FirstOrDefaultAsync(
                a => a.Id == senderId && a.UserId == userId);

            if (sender == null)
                throw new KeyNotFoundException("Sender account not found.");

            var receiver = await _context.Accounts.FirstOrDefaultAsync(
                a => a.Id == receiverId);

            if (receiver == null)
                throw new KeyNotFoundException("Receiver account not found.");

     
            Guid transferReferenceId = Guid.NewGuid();

            sender.Withdraw(amount);

            bool result = receiver.Deposit(amount);

            if (!result)
                throw new ArgumentException(
                    "Transfer amount must be greater than zero.");

          
            var senderTransaction = new Transaction(
                sender.Id,
                amount,
                TransactionType.TransferSent,
                transferReferenceId);

            var receiverTransaction = new Transaction(
                receiver.Id,
                amount,
                TransactionType.TransferReceived,
                transferReferenceId);

            _context.Transactions.Add(senderTransaction);
            _context.Transactions.Add(receiverTransaction);

          
            var idempotencyRecord = new IdempotencyRecord
            {
                UserId = userId,
                IdempotencyKey = idempotencyKey,
                SenderAccountId = senderId,
                ReceiverAccountId = receiverId,
                Amount = amount,
                SenderBalance = sender.Balance,
                ReceiverBalance = receiver.Balance,
                CreatedAt = DateTime.UtcNow
            };

            _context.IdempotencyRecords.Add(idempotencyRecord);

            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new TransferResultDto
            {
                SenderBalance = sender.Balance,
                ReceiverBalance = receiver.Balance
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TransactionHistoryResponseDto?> GetTransactionHistoryAsync( int accountId, int userId, int page,    int pageSize)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            a => a.Id == accountId && a.UserId == userId);

        if (account == null)
            return null;

        if (page < 1)
            throw new ArgumentException("Page must be greater than or equal to 1.");

        if (pageSize < 1 || pageSize > 20)
            throw new ArgumentException("Page size must be between 1 and 20.");

        var query = _context.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.CreatedAt)
            .ThenByDescending(t => t.Id);

        var totalRecord = await query.CountAsync();

        var transactions = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Type = t.Type,
                TransferReferenceId = t.TransferReferenceId,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(
            (double)totalRecord / pageSize);

        return new TransactionHistoryResponseDto
        {
            AccountId = accountId,
            CurrentPage = page,
            PageSize = pageSize,
            TotalRecord = totalRecord,
            TotalPages = totalPages,
            Transactions = transactions
        };
    }
}