using Microsoft.AspNetCore.Mvc;
using QuoteSystem.Models;

namespace QuoteSystem.Controllers
{
    public class QuotesController : Controller
    {
        public IActionResult Index()
        {

            return View(temp);
        }

        public IActionResult EditR()
        {
            return View();
        }

        public IActionResult PreviewR()
        {
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Quote quote)
        {

            return View(temp);
        }

        private static readonly List<Quote> temp = new()
        {
            new Quote()
            {
                CustomerInfo = new()
                {
                    Name = "Noah Tomkins",
                    Address = "1287 Treeland St, Burlington Ontario",
                }
            }
        };
    }
}
