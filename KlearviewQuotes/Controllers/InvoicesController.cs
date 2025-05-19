using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace KlearviewQuotes.Controllers
{
    [Authorize(Roles = "Admin,AlwaysAdmin,QuoteEditor")]
    public class InvoicesController : Controller
    {
        private readonly IAppDataRepository _repository;

        private static readonly int pageSize = 20;

        public InvoicesController(IAppDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int? invoicePage)
        {
            var invocies = await _repository.GetInvoicesAsync();

            if (invocies == null)
                return NotFound();

            return View(invocies.ToPagedList(invoicePage ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var invoice = await _repository.GetInvoiceAsync(id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }
    }
}
