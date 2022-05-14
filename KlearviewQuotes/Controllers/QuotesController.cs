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
        public async Task<IActionResult> Index()
        {
            var quotes = await _repository.GetAllQuotesAsync();

            return View(quotes);
        }

        // GET: Quotes/Edit/{id}
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View(quote);
        }

        // GET: Quotes/Edit
        public IActionResult NewQuote()
        {
            return View(nameof(Edit), new Quote());
        }

        // GET: Quotes/Preview/{id}
        public async Task<IActionResult> Preview(long? id)
        {
            if (id == null || id <= 0)
                return View(new Quote());

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View(quote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEdit(Quote quote)
        {
            if (quote.Id == null)
                await _repository.AddQuoteAsync(quote);
            else
                await _repository.UpdateQuoteAsync(quote);

            return RedirectToAction(nameof(Index));
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
