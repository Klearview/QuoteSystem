using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class ServiceLocation : Location
    {
        [Key]
        public string? ServiceLocationId { get; set; }

        public string? AccountId { get; set; }
        public string? BusinessUnitId { get; set; }
        public string? DefaultContactId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Active { get; set; }
        public string? TaxRate { get; set; }

        public string? TimeZoneId { get; set; }
        public string? ZoneId { get; set; }

        [Display(Name = "Memo")]
        public string? ServiceMemo { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Zone? Zone { get; set; }
        public virtual IList<Agreement> Agreements { get; set; }
        public virtual IList<WorkOrder> WorkOrders { get; set; }
        public virtual Contact? DefaultContact { get; set; }

        public virtual Account? DefaultAccount { get; set; }
        public virtual IList<UserDefinedFieldValue> UserDefinedFieldValues { get; set; }

    }
}
