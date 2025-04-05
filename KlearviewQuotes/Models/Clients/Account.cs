using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class Account
    {
        [Key]
        public string? AccountId { get; set; }

        public string? BuisnessUnitId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Active { get; set; }
        public string? TaxExempt { get; set; }
        public string? DefaultServiceLocationId { get; set; }
        public string? DefaultBillingLocationId { get; set; }
        public string? AccountType { get; set; }
    }
}
