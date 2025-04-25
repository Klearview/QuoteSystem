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
        public virtual DbSet<ContactEmail> ContactEmails { get; set; }
        public virtual DbSet<ContactPhone> ContactPhones { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Campaign> MarketingCampaigns { get; set; }
        public virtual DbSet<WorkOrderService> WorkOrderServices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PrepareAccounts(modelBuilder);
            PrepareWorkOrders(modelBuilder);
            PrepareServiceLocations(modelBuilder);
            PrepareBillingLocations(modelBuilder);
            PrepareContacts(modelBuilder);
        }

        private static void PrepareWorkOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrder>()
                .HasOne(e => e.Resource)
                .WithMany(e => e.WorkOrders)
                .HasForeignKey(e => e.ResourceId)
                .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<WorkOrder>()
                .HasOne(e => e.CommittedResource)
                .WithMany(e => e.WorkOrdersCommited)
                .HasForeignKey(e => e.CommittedResourceId)
                .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<WorkOrder>()
                .HasOne(e => e.PerformedByResource)
                .WithMany(e => e.WorkOrdersPerformed)
                .HasForeignKey(e => e.PerformedByResourceId)
                .HasPrincipalKey(e => e.TeamId);

            modelBuilder.Entity<WorkOrder>()
                .HasOne(e => e.Campaign)
                .WithMany(e => e.WorkOrders)
                .HasForeignKey(e => e.CampaignId)
                .HasPrincipalKey(e => e.CampaignId);

            modelBuilder.Entity<WorkOrder>()
                .HasMany(e => e.WorkOrderServices)
                .WithOne(e => e.WorkOrder)
                .HasForeignKey(e => e.WorkOrderId)
                .HasPrincipalKey(e => e.WorkOrderId);
        }

        private static void PrepareContacts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
               .HasMany(e => e.ContactPhones)
               .WithOne(e => e.Contact)
               .HasForeignKey(e => e.ContactId)
               .HasPrincipalKey(e => e.ContactId);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.ContactEmails)
                .WithOne(e => e.Contact)
                .HasForeignKey(e => e.ContactId)
                .HasPrincipalKey(e => e.ContactId);

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.BillingLocation)
                .WithOne(e => e.DefaultContact)
                .HasForeignKey<BillingLocation>(e => e.DefaultContactId)
                .HasPrincipalKey<Contact>(e => e.ContactId);

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.ServiceLocation)
                .WithOne(e => e.DefaultContact)
                .HasForeignKey<ServiceLocation>(e => e.DefaultContactId)
                .HasPrincipalKey<Contact>(e => e.ContactId);
        }

        private static void PrepareServiceLocations(ModelBuilder modelBuilder)
        {
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
        }

        private static void PrepareBillingLocations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingLocation>()
                .HasMany(e => e.WorkOrders)
                .WithOne(e => e.BillingLocation)
                .HasForeignKey(e => e.BillingLocationId)
                .HasPrincipalKey(e => e.BillingLocationId);
        }

        private static void PrepareAccounts(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<Account>()
                .HasOne(e => e.DefualtBillingLocation)
                .WithOne(e => e.DefaultAccount)
                .HasForeignKey<BillingLocation>(e => e.BillingLocationId)
                .HasPrincipalKey<Account>(e => e.DefaultBillingLocationId);
        }
    }
}
