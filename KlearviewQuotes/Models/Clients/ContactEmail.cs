namespace KlearviewQuotes.Models.Clients
{
    public class ContactEmail
    {
        public string? ContactEmailId { get; set; }
        public string? ContactId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
