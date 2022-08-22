using System.Net.Mail;

namespace KlearviewQuotes.Services.Interfaces
{
    public interface IEmailService
    {
        void SendTextOnlyEmail(MailAddress recipient, string subject, string body);
    }
}
