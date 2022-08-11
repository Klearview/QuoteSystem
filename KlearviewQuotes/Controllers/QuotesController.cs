using KlearviewQuotes.Models;
using KlearviewQuotes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KlearviewQuotes.Controllers
{
    [Authorize(Roles = "Admin,AlwaysAdmin,QuoteEditor")]
    public class QuotesController : Controller
    {
        private readonly IAppDataRepository _repository;

        public QuotesController(IAppDataRepository repository)
        {
            _repository = repository;
        }

        // GET: Quotes
        public async Task<IActionResult> Index(string? searchString)
        {
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
            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View(new QuoteEdit(quote));
        }

        // GET: Quotes/Edit
        public IActionResult NewQuote()
        {
            return View(nameof(Edit), new QuoteEdit());
        }

        // GET: Quotes/Preview/{id}
        [HttpGet]
        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null || id <= 0)
                return View(new Quote());

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            //return View(quote);
            return PartialView("_PreviewModal", quote);
        }

        // GET: Quotes/PreviewPrint/{id}
        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuoteEdit quoteEdit)
        {
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
