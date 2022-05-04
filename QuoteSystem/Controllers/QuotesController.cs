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
            return View(test);
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
                QuoteNumber = 1,
                CustomerInfo = new()
                {
                    Name = "Noah Tomkins",
                    Address = "1287 Treeland St",
                    City = "Burlington",
                    PostalCode = "L7R 3T5"
                },
                Date = DateTime.Now,
                Accepted = true
            }
        };

        private static readonly Quote test = new()
        {
            QuoteNumber = 1,
            Date = DateTime.Now,
            Accepted = false,
            CustomerInfo = new()
            {
                Name = "Noah Tomkins",
                Address = "1287 Treeland St",
                City = "Burlington",
                PostalCode = "L7R 3T5",
                Phone = "9053202808",
                Cell = "9053202808",
                Email = "1tomkinsnoa@gmail.com"
            },
            SalesRep = "David Tomkins",
            Description = "Test Description",
            Notes = "Private Notes",
            Estimate = new()
            {
                Exterior = new()
                {
                    ExteriorPrice = 10,
                    ScreensPrice = 20,
                    HighWindowsPrice = 30
                },
                ExteriorAndInterior = new()
                {
                    Total = 40,
                    Basements = new()
                    {
                        Price = 50,
                        WalkoutOnly = true
                    },
                    Skylights = new()
                    {
                        Price = 60,
                        InAndOut = true,
                        Out = false
                    },
                    DeckGlassPrice = 70,
                },
                SidingPrice = 80,
                EvestroughPrice = 90
            }
        };
    }
}
