using Microsoft.EntityFrameworkCore;
using QuoteSystem.Models;

namespace QuoteSystem.Services
{
    public class AppDataContext : DbContext
    {
        public virtual DbSet<Quote> Quotes { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>()
                .HasKey(x => x.QuoteNumber);
        }
    }
}
