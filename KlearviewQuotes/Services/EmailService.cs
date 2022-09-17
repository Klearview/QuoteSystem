using KlearviewQuotes.Models;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace KlearviewQuotes.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailAddress _fromAddress;

        private readonly SmtpClient _smtpClient;
        private readonly ApiSettings _apiSettings;

        public EmailService(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;

            _fromAddress = _apiSettings.Email!.Sender!.MailAddress!;
            var cred = _apiSettings.Email!.Sender!.NetworkCredential;
            

            _smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = cred
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

        public bool SendEmailWithPDF(MailAddress recipient, string subject, string body, PDF pdf, bool html = false)
        {
            using (var message = new MailMessage(_fromAddress, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = html
            })
            {
                if (pdf.Attachment == null)
                    return false;

                message.Attachments.Add(pdf.Attachment);
                _smtpClient.Send(message);

                return true;
            }
        }

        public bool SendDefaultEmailWithPDF(MailAddress recipient, PDF pdf)
        {
            string subject = "Estimate";

            var path = $"{Directory.GetCurrentDirectory()}\\wwwroot\\EmailBody.html";
            var body = File.ReadAllText(path);

            return SendEmailWithPDF(recipient, subject, body, pdf, true);
        }
    }
}
