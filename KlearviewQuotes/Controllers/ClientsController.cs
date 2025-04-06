using KlearviewQuotes.Models;
using KlearviewQuotes.Models.Clients;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace KlearviewQuotes.Controllers
{
    [Authorize(Roles = "Admin,AlwaysAdmin,QuoteEditor")]
    public class ClientsController : Controller
    {
        private readonly IAppDataRepository _repository;

        private static readonly int pageSize = 20;

        public ClientsController(IAppDataRepository repository)
        {
            _repository = repository;
        }

        #region Client

        // GET: Clients
        public async Task<IActionResult> Index(string searchString, string sortOrder, int? page)
        {
            AddSortOrderViewBag(sortOrder);
            ViewBag.CurrentSearchString = searchString;

            var accounts = await _repository.GetAccountsAsync();

            if (accounts == null)
                return NotFound();

            if (!string.IsNullOrEmpty(searchString))
                accounts = accounts.Where(s => s.Contains(searchString)).ToList();

            accounts = SortAccountsList(accounts, sortOrder);

            int pageNumber = (page ?? 1);

            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clients/Client/{id}
        public async Task<IActionResult> Client(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var account = await _repository.GetAccountAsync(id);

            if (account == null)
                return NotFound();

            return View(account);
        }

        #endregion

        #region Sorting And Filtering

        private void AddSortOrderViewBag(string sortOrder)
        {
            ViewBag.NumberParm = String.IsNullOrEmpty(sortOrder) ? "" : "number_asc";
            ViewBag.NameParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
        }

        private IList<Account> SortAccountsList(IList<Account> accounts, string sortOrder)
        {
            switch (sortOrder)
            {
                case "number_asc":
                    accounts = accounts.OrderBy(s => s.Number).ToList();
                    break;
                case "name_asc":
                    accounts = accounts.OrderBy(s => s.Name).ToList();
                    break;
                case "name_desc":
                    accounts = accounts.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    accounts = accounts.OrderByDescending(s => s.Number).ToList();
                    break;
            }

            return accounts;
        }

        #endregion
    }
}
