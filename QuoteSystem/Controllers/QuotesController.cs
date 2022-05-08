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


        public async Task<IActionResult> Index()
        {
            var quotes = await _repository.GetAllQuotesAsync();

            return View(quotes);
        }

        public IActionResult EditR()
        {
            return View();
        }

        public IActionResult PreviewR()
        {
            return View(test);
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
