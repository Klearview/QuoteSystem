using DinkToPdf;
using DinkToPdf.Contracts;
using KlearviewQuotes.Services.Interfaces;
using SelectPdf;
using System.Net;

namespace KlearviewQuotes.Services
{
    public class PDFService : IPDFService
    {
        private readonly IConverter _converter;

        public PDFService(IConverter converter)
        {
            _converter = converter;
        }

        public async Task<byte[]?> ConvertPreviewToPDF(int id)
        {
            using (HttpClient client = new())
            {
                string html = await client.GetStringAsync($"https://localhost:7145/Quotes/Print/{id}");

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

                Stream stream = new MemoryStream(pdf);
                pdfDocument.Close();

                return pdf;
            }

            /*var globalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.Letter,
                DocumentTitle = "Quote"
            };

            using (HttpClient client = new())
            {
                string html = await client.GetStringAsync($"https://localhost:7145/Quotes/Print/{id}");

                var objectSettings = new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = "https://localhost:7145/css/preview.css" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                return _converter.Convert(pdf);
            }*/
        }
    }
}
