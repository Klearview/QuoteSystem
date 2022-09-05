namespace KlearviewQuotes.Models.ViewModels
{
    public class QuoteEditViewModel
    {
        public Quote Quote { get; set; }
        public string? LastOption { get; set; }
        public string? SubmitOption { get; set; }

        public QuoteEditViewModel(Quote quote)
        {
            Quote = quote;
        }

        public QuoteEditViewModel() : this(new Quote())
        {

        }
    }
}
