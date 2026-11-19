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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<RefreshToken>()
            .Property(r => r.TokenHash)
            .HasMaxLength(64)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
