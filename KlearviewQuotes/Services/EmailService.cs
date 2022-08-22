using KlearviewQuotes.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace KlearviewQuotes.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailAddress _fromAddress = new("1tomkinsnoa@gmail.com", "Noah Tomkins");
        private readonly string _fromPassword = "eejibskljkuinnjo";


        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromAddress.Address, _fromPassword)
            };
        }

        public void SendTextOnlyEmail(MailAddress recipient, string subject, string body)
        {
            using (var message = new MailMessage(_fromAddress, recipient)
            {
                Subject = subject,
                Body = body
            })
            {
                _smtpClient.Send(message);
            }
        }
    }
}
