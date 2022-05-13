using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KlearviewQuotes.Models;

namespace KlearviewQuotes.Data
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
                .HasKey(x => x.Id);
        }
    }
}
