using KlearviewQuotes.Models;
using KlearviewQuotes.Models.ViewModels;
using KlearviewQuotes.Services;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Options;

namespace KlearviewQuotes.Controllers
{
    [Authorize(Roles = "Admin,AlwaysAdmin,QuoteEditor")]
    public class QuotesController : Controller
    {
        private readonly IAppDataRepository _repository;
        private readonly IPDFService _pdfService;
        private readonly IEmailService _emailService;
        private readonly ApiSettings _settings;

        public QuotesController(IAppDataRepository repository, IPDFService pDFService, IEmailService emailService, IOptions<ApiSettings> settings)
        {
            _repository = repository;
            _pdfService = pDFService;
            _emailService = emailService;
            _settings = settings.Value;
        }

        // GET: Quotes
        public async Task<IActionResult> Index(string? searchString)
        {
            await AddStatusListViewBag();

            var quotes = await _repository.GetAllQuotesAsync();

            if (quotes == null)
                return NotFound();

            try
            {
                if (!string.IsNullOrEmpty(searchString))
                    return View(quotes.Where(s => s.CustomerInfo.Contains(searchString)));
            } 
            catch
            {
                return View(quotes);
            }

            return View(quotes);
        }

        // GET: Quotes/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            await AddStatusListViewBag();

            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View(new QuoteEditViewModel(quote));
        }

        // GET: Quotes/Edit
        public async Task<IActionResult> NewQuote()
        {
            await AddStatusListViewBag();

            return View(nameof(Edit), new QuoteEditViewModel());
        }

        // GET: Quotes/Preview/{id}
        [HttpGet]
        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            var pdfUrl = $"{_settings.BaseUrl}/Quotes/PDF/{id.Value}";

            var previewViewModel = new PreviewViewModel()
            {
                Quote = quote,
                PDFUrl = pdfUrl
            };

            //return View(quote);
            return PartialView("_PreviewModal", previewViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Email(int? id)
        {
            if (id == null || id <= 0)
                return PartialView("_EmailConfirmation", null);

            var quote = await _repository.GetQuoteAsync(id.Value);

            return PartialView("_EmailConfirmation", quote);
        }

        // GET: Quotes/PreviewPrint/{id}
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Print(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View("_PreviewPrint", quote);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            if (quote.CustomerInfo == null || quote.CustomerInfo.Email == null)
                return BadRequest();

            var pdf = await _pdfService.ConvertPreviewToPDF(id.Value);

            if (pdf == null || pdf.Attachment == null)
                return NotFound();

            /*var email = _emailService.SendEmailWithPDF(
                new(quote.CustomerInfo.Email),
                "Estimate",
                "",
                pdf
                );
            */

            var email = _emailService.SendDefaultEmailWithPDF(new(quote.CustomerInfo.Email), pdf);

            if (email)
            {
                quote.SentAt = DateTime.Now;
                await _repository.UpdateQuoteAsync(quote);

                return Ok();
            }
                

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuoteEditViewModel quoteEdit)
        {
            await AddStatusListViewBag();

            quoteEdit.LastOption = quoteEdit.SubmitOption;
            Quote quote = quoteEdit.Quote;

            switch (quoteEdit.SubmitOption)
            {
                case "save":
                    await SaveQuote(quote);
                    return RedirectToAction(nameof(Index));
                case "preview":
                    await SaveQuote(quote);
                    return View(quoteEdit);
                case "cancel":
                default:
                    return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> PDF(int id)
        {
            var pdf = await _pdfService.ConvertPreviewToPDF(id);

            if (pdf == null || pdf.Data == null)
                return NotFound();

            return File(pdf.Data, "application/pdf");
        }

        private async Task SaveQuote(Quote quote)
        {
            string? username = null;

            if (HttpContext.User.Identity != null)
                username = HttpContext.User.Identity.Name;

            if (quote.Id == null)
            {
                quote.CreatedBy = username;
                quote.CreatedAt = DateTime.Now;

                await _repository.AddQuoteAsync(quote);
            }
            else
            {
                quote.UpdatedBy = username;
                quote.UpdatedAt = DateTime.Now;

                await _repository.UpdateQuoteAsync(quote);
            }         
        }

        private async Task AddStatusListViewBag()
        {
            var status = await _repository.GetStatusAsync();
            SelectList selectList = new SelectList(status, "Name", "Name");

            ViewBag.Status = selectList;
        }


        private static readonly Quote test = new()
        {
            CustomerInfo = new()
            {
                Name = "Noah Tomkins",
                Address = "1287 Treeland st Burlington",
                Phone = "9053202808",
                Cell = "9053202808",
                Email = "1tomkinsnoa@gmail.com"
            }
        };

        private static readonly List<Quote> testList = new()
        {
            test
        };
    }
}
