namespace KlearviewQuotes.Models.Clients
{
    public class UserDefinedField
    {
        public string? Id { get; set; }
        public string? Label { get; set; }
        public string? Entity { get; set; }

        public virtual IList<UserDefinedFieldValue> UserDefinedFieldValues { get; set; }
    }
}
