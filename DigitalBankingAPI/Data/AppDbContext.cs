using DigitalBankingAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DigitalBankingAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<IdempotencyRecord> IdempotencyRecords { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(a => a.Balance)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(t => t.Amount)
                .HasPrecision(18, 2);

            entity.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RefreshToken>()
            .Property(r => r.TokenHash)
            .HasMaxLength(64)
            .IsRequired();
        modelBuilder.Entity<IdempotencyRecord>(entity =>
        {
            entity.Property(i => i.IdempotencyKey)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(i => i.Amount)
                .HasPrecision(18, 2);

            entity.Property(i => i.SenderBalance)
                .HasPrecision(18, 2);

            entity.Property(i => i.ReceiverBalance)
                .HasPrecision(18, 2);

            entity.HasIndex(i => new { i.UserId, i.IdempotencyKey })
                .IsUnique();
        });
        base.OnModelCreating(modelBuilder);
    }
}
