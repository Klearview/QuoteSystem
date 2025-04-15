using KlearviewQuotes.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KlearviewQuotes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ServiceLocation> ServiceLocations { get; set; }
        public virtual DbSet<BillingLocation> BillingLocations { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.ServiceLocations)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .HasPrincipalKey(e => e.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Agreements)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .HasPrincipalKey(e => e.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.WorkOrders)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .HasPrincipalKey(e => e.AccountId);

            modelBuilder.Entity<Account>()
                .HasOne(e => e.DefaultServiceLocation)
                .WithOne(e => e.DefaultAccount)
                .HasForeignKey<ServiceLocation>(e => e.ServiceLocationId)
                .HasPrincipalKey<Account>(e => e.DefaultServiceLocationId);

            modelBuilder.Entity<ServiceLocation>()
                .HasOne(e => e.Zone)
                .WithMany(e => e.ServiceLocations)
                .HasForeignKey(e => e.ZoneId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<ServiceLocation>()
                .HasMany(e => e.Agreements)
                .WithOne(e => e.ServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .HasPrincipalKey(e => e.ServiceLocationId);

            modelBuilder.Entity<ServiceLocation>()
                .HasMany(e => e.WorkOrders)
                .WithOne(e => e.ServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .HasPrincipalKey(e => e.ServiceLocationId);

            modelBuilder.Entity<BillingLocation>()
                .HasMany(e => e.WorkOrders)
                .WithOne(e => e.BillingLocation)
                .HasForeignKey(e => e.BillingLocationId)
                .HasPrincipalKey(e => e.BillingLocationId);

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.ServiceLocation)
                .WithOne(e => e.DefaultContact)
                .HasForeignKey<ServiceLocation>(e => e.DefaultContactId)
                .HasPrincipalKey<Contact>(e => e.ContactId);

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.BillingLocation)
                .WithOne(e => e.DefaultContact)
                .HasForeignKey<BillingLocation>(e => e.DefaultContactId)
                .HasPrincipalKey<Contact>(e => e.ContactId);
        }
    }
}
