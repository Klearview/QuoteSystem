using DinkToPdf;
using DinkToPdf.Contracts;
using KlearviewQuotes.Models;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.Extensions.Options;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using SelectPdf;
using System.Net;
using System.Runtime.InteropServices;

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
            string pageUrl = $"{_settings.BaseUrl}/Quotes/Print/{id}";

            //return await ConvertPreviewToPDF(pageUrl);
            return await ConvertPDFUsingPuppeteer(pageUrl);
        }

        private async Task<PDF?> CovertPDFUsingSelectPDF(string pageUrl)
        {
            using (HttpClient client = new())
            {
                string html = await client.GetStringAsync(pageUrl);

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

        private async Task<PDF?> ConvertPDFUsingPuppeteer(string pageUrl)
        {
            using BrowserFetcher fetcher = new();

            await fetcher.DownloadAsync();

            LaunchOptions launchOptions = new()
            {
                Headless = true
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //launchOptions.ExecutablePath = "/usr/bin/chromium-browser";
            }

            await using var browser = await Puppeteer.LaunchAsync(launchOptions);
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(pageUrl);
            await page.EvaluateExpressionHandleAsync("document.fonts.ready");

            byte[] pdf = await page.PdfDataAsync(new()
            {
                MarginOptions = new()
                {
                    Top = "0.3in",
                    Right = "0.2in",
                    Bottom = "0in",
                    Left = "0.2in"
                },
                Format = PaperFormat.Letter,
                PrintBackground = true
            });

            return new PDF()
            {
                Name = "Klearview Estimate.pdf",
                Data = pdf
            };
        }
    }
}
