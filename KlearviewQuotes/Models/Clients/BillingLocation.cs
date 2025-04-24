using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class BillingLocation : Location
    {
        public string? BillingLocationId { get; set; }
        public string? AccountId { get; set; }
        public string? BusinessUnitId { get; set; }
        public string? DefaultContactId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? PreferredMethodOfDelivery { get; set; }
        public string? Active { get; set; }

        [Display(Name = "Memo")]
        public string? BillingMemo { get; set; }

        public virtual IList<WorkOrder> WorkOrders { get; set; }
        public virtual Contact DefaultContact { get; set; }
        public virtual Account DefaultAccount { get; set; }
    }
}
