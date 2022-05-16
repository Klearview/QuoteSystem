namespace KlearviewQuotes.Models
{
    public class QuoteEdit
    {
        public Quote Quote { get; set; }
        public string? LastOption { get; set; }
        public string? SubmitOption { get; set; }

        public QuoteEdit(Quote quote)
        {
            Quote = quote;
        }

        public QuoteEdit() : this(new Quote())
        {

        }
    }
}
