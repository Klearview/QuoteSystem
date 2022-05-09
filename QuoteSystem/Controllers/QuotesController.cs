using Microsoft.AspNetCore.Mvc;
using QuoteSystem.Models;
using QuoteSystem.Services;

namespace QuoteSystem.Controllers
{
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
                return View(new Quote());

            var quote = await _repository.GetQuoteAsync(id.Value);

            if (quote == null)
                return NotFound();

            return View(quote);
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
        public async Task<IActionResult> Index(Quote quote)
        {
            await _repository.AddQuoteAsync(quote);
            var quotes = await _repository.GetAllQuotesAsync();

            return View(quotes);
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
