using Microsoft.EntityFrameworkCore;

namespace FinancesAPI.Models
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
    }
}
