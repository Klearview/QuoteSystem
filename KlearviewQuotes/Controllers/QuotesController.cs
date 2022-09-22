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

        #region List

        // GET: Quotes
        public async Task<IActionResult> Index(string searchString, string status, string sortOrder)
        {
            await AddStatusListViewBag();
            AddSortOrderViewBag(sortOrder);

            var quotes = await _repository.GetAllQuotesAsync();

            if (quotes == null)
                return NotFound();

            if (!string.IsNullOrEmpty(searchString))
                quotes = quotes.Where(s => s.CustomerInfo.Contains(searchString)).ToList();

            if (!string.IsNullOrEmpty(status))
                quotes = quotes.Where(s => s.Status != null && s.Status.Contains(status)).ToList();

            quotes = SortList(quotes, sortOrder);

            return View(quotes);
        }

        #endregion List

        #region Editing

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

        // GET: Quotes/Edit
        public async Task<IActionResult> NewQuote()
        {
            await AddStatusListViewBag();

            return View(nameof(Edit), new QuoteEditViewModel());
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

        #endregion Editing

        #region Preview

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

            quote.QuoteInfo.DateOfEstimate = DateTime.Now;

            var previewViewModel = new PreviewViewModel()
            {
                Quote = quote,
                PDFUrl = pdfUrl
            };

            //return View(quote);
            return PartialView("_PreviewModal", previewViewModel);
        }

        // GET: Quotes/PreviewPrint/{id}
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Print(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);
            quote.QuoteInfo.DateOfEstimate = DateTime.Now;

            if (quote == null)
                return NotFound();

            return View("_PreviewPrint", quote);
        }

        public async Task<IActionResult> PDF(int id)
        {
            var pdf = await _pdfService.ConvertPreviewToPDF(id);

            if (pdf == null || pdf.Data == null)
                return NotFound();

            return File(pdf.Data, "application/pdf");
        }

        #endregion Preview

        #region Email

        [HttpGet]
        public async Task<IActionResult> Email(int? id)
        {
            if (id == null || id <= 0)
                return PartialView("_EmailConfirmation", null);

            var quote = await _repository.GetQuoteAsync(id.Value);

            return PartialView("_EmailConfirmation", quote);
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

        #endregion Email

        #region Sorting And Filtering

        private async Task AddStatusListViewBag()
        {
            var status = await _repository.GetStatusAsync();
            SelectList selectList = new SelectList(status, "Name", "Name");

            ViewBag.Status = selectList;
        }

        private void AddSortOrderViewBag(string sortOrder)
        {
            ViewBag.CreatedOnParm = String.IsNullOrEmpty(sortOrder) ? "created_asc" : "";
            ViewBag.AddressSortParm = sortOrder == "address_asc" ? "address_desc" : "address_asc";
            ViewBag.UpdatedAtParm = sortOrder == "updated_asc" ? "updated_desc" : "updated_asc";
            ViewBag.SentOnParm = sortOrder == "senton_asc" ? "senton_desc" : "senton_asc";
            ViewBag.StatusParm = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            ViewBag.NameParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.AccountParm = sortOrder == "account_asc" ? "account_desc" : "account_asc";
        }

        private IList<Quote> SortList(IList<Quote> quotes, string sortOrder)
        {
            switch (sortOrder)
            {
                case "created_asc":
                    quotes = quotes.OrderBy(s => s.CreatedAt).ToList();
                    break;
                case "address_asc":
                    quotes = quotes.OrderBy(s => s.CustomerInfo.Address).ToList();
                    break;
                case "address_desc":
                    quotes = quotes.OrderByDescending(s => s.CustomerInfo.Address).ToList();
                    break;
                case "updated_asc":
                    quotes = quotes.OrderBy(s => s.UpdatedAt).ToList();
                    break;
                case "updated_desc":
                    quotes = quotes.OrderByDescending(s => s.UpdatedAt).ToList();
                    break;
                case "senton_asc":
                    quotes = quotes.OrderBy(s => s.SentAt).ToList();
                    break;
                case "senton_desc":
                    quotes = quotes.OrderByDescending(s => s.SentAt).ToList();
                    break;
                case "status_asc":
                    quotes = quotes.OrderBy(s => s.Status).ToList();
                    break;
                case "status_desc":
                    quotes = quotes.OrderByDescending(s => s.Status).ToList();
                    break;
                case "name_asc":
                    quotes = quotes.OrderBy(s => s.CustomerInfo.Name).ToList();
                    break;
                case "name_desc":
                    quotes = quotes.OrderByDescending(s => s.CustomerInfo.Name).ToList();
                    break;
                case "account_asc":
                    quotes = quotes.OrderBy(s => s.CustomerInfo.AccountNumber).ToList();
                    break;
                case "account_desc":
                    quotes = quotes.OrderByDescending(s => s.CustomerInfo.AccountNumber).ToList();
                    break;
                default:
                    quotes = quotes.OrderByDescending(s => s.CreatedAt).ToList();
                    break;
            }

            return quotes;
        }

        #endregion Sorting And Filtering

        #region Test

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

        #endregion Test
    }
}
