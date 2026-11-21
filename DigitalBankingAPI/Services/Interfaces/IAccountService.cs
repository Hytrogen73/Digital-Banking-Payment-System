using DigitalBankingAPI.DTOs.Responses;
using DigitalBankingAPI.Models;

namespace DigitalBankingAPI.Services.Interfaces;

public interface IAccountService
{
    Task<Account?> GetAccountAsync(int accountId, int userId);

    Task<Account> CreateAccountAsync(int userId, decimal balance);

    Task<Account?> DepositAsync(int accountId, int userId, decimal amount);

    Task<Account?> WithdrawAsync(int accountId, int userId, decimal amount);

    Task<TransferResultDto> TransferAsync(
     int senderId,
     int receiverId,
     int userId,
     decimal amount,
     string idempotencyKey);

    Task<TransactionHistoryResponseDto?> GetTransactionHistoryAsync(
        int accountId,
        int userId,
        int page,
        int pageSize);
}