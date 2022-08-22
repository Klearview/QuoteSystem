using DinkToPdf;
using KlearviewQuotes.Services;
using System.Net.Mail;

namespace KlearviewQuotes_Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_TextOnlyEmail()
        {
            EmailService emailService = new();

            MailAddress recipient = new("1tomkinsnoa@gmail.com", "Noah Tomkins");

            emailService.SendTextOnlyEmail(recipient, "Test", "Test Message");
        }

        [TestMethod]
        public async Task Test_CreatePdfFromPreview()
        {
            SynchronizedConverter syncConv = new(new PdfTools());
            PDFService pdfService = new(syncConv);

            var a = await pdfService.ConvertPreviewToPDF(0);
        }
    }
}