using DinkToPdf;
using DinkToPdf.Contracts;
using KlearviewQuotes.Models;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.Extensions.Options;
using SelectPdf;
using System.Net;

namespace KlearviewQuotes.Services
{
    public class PDFService : IPDFService
    {

        private readonly ApiSettings _settings;

        public PDFService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<PDF?> ConvertPreviewToPDF(int id)
        {
            using (HttpClient client = new())
            {
                string html = await client.GetStringAsync($"{_settings.BaseUrl}/Quotes/Print/{id}");

                HtmlToPdf htmlToPdf = new()
                {
                    Options = {
                        PdfPageOrientation = PdfPageOrientation.Portrait,
                        RenderingEngine = RenderingEngine.WebKit,
                        MarginBottom = 0,
                        MarginLeft = 5,
                        MarginRight = 5,
                        MarginTop = 7,
                        PdfPageSize = PdfPageSize.Letter
                    }
                };

                //htmlToPdf.Options.CustomCSS = "https://localhost:7145/wwwroot/css/preview.css";

                PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
                byte[] pdf = pdfDocument.Save();

                pdfDocument.Close();

                return new PDF()
                {
                    Name = "Klearview Estimate",
                    Data = pdf
                };
            }
        }
    }
}
