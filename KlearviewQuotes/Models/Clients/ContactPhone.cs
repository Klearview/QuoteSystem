namespace KlearviewQuotes.Models.Clients
{
    public class ContactPhone
    {
        public string? ContactPhoneId { get; set; }
        public string? ContactId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Extension { get; set; }

        public virtual Contact? Contact { get; set; }
    }
}
