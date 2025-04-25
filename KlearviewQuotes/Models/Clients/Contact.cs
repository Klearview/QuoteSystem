namespace KlearviewQuotes.Models.Clients
{
    public class Contact : Location
    {
        public string? ContactId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Company { get; set; }
        public string? Active { get; set; }

        public virtual ServiceLocation ServiceLocation { get; set; }
        public virtual BillingLocation BillingLocation { get; set; }

        public virtual IList<ContactEmail> ContactEmails { get; set; }
        public virtual IList<ContactPhone> ContactPhones { get; set; }
    }
}
