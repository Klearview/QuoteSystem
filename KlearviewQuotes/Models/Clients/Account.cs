using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using X.PagedList;

namespace KlearviewQuotes.Models.Clients
{
    public class Account
    {
        [Key]
        public string? AccountId { get; set; }

        public string? BusinessUnitId { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Account Number")]
        public int? Number { get; set; }
        public string? Active { get; set; }

        [Display(Name = "Tax Exempt")]
        public string? TaxExempt { get; set; }
        public string? DefaultServiceLocationId { get; set; }
        public string? DefaultBillingLocationId { get; set; }

        [Display(Name = "Account Type")]
        public string? AccountType { get; set; }

        public virtual IList<ServiceLocation> ServiceLocations { get; set; }
        public virtual IList<Agreement> Agreements { get; set; }
        public virtual IList<WorkOrder> WorkOrders { get; set; }

        public virtual ServiceLocation DefaultServiceLocation { get; set; }
        public virtual BillingLocation DefualtBillingLocation { get; set; }

        public bool Contains(string search)
        {
            search = FormatString(search);

            return (
                (Name != null && FormatString(Name).Contains(search)) ||
                (Number != null && FormatString(Number.Value.ToString()).Contains(search))
            );
        }

        private string FormatString(string input)
        {
            input = input.ToLower();

            string regExp = "\\W";
            return Regex.Replace(input, regExp, "");
        }
    }
}
