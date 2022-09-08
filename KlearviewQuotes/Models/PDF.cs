using System.Net.Mail;

namespace KlearviewQuotes.Models
{
    public class PDF
    {
        public string? Name { get; set; }
        public byte[]? Data { get; set; }

        public Attachment? Attachment
        {
            get
            {
                if (Name == null || Data == null)
                    return null;

                var stream = new MemoryStream(Data);
                return new Attachment(stream, Name, "application/pdf");
            }
        }
    }
}
