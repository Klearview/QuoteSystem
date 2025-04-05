using KlearviewQuotes.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KlearviewQuotes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
