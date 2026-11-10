using DigitalBankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankingAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
}
