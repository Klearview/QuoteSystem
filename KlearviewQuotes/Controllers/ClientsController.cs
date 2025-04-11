using KlearviewQuotes.Models;
using KlearviewQuotes.Models.Clients;
using KlearviewQuotes.Models.ViewModels;
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
        private static readonly int clientPageSize = 10;

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

            accounts = SortAccounts(accounts, sortOrder);

            int pageNumber = (page ?? 1);

            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clients/Client/{id}
        public async Task<IActionResult> Client(string? id, int? agreementPage, int? workOrderPage, string woSort)
        {
            WorkOrdersController.AddSortOrderViewBag(ViewBag, woSort);
            ViewBag.CurrentAgreementPage = agreementPage ?? 1;
            ViewBag.CurrentWorkOrderPage = workOrderPage ?? 1;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var account = await _repository.GetAccountAsync(id);

            if (account == null)
                return NotFound();

            account.WorkOrders = WorkOrdersController.SortWorkOrders(account.WorkOrders, woSort);

            ClientViewModel cvm = new(account)
            {
                PagedAgreements = account.Agreements.ToPagedList(agreementPage ?? 1, clientPageSize),
                PagedWorkOrders = account.WorkOrders.ToPagedList(workOrderPage ?? 1, clientPageSize)
            };

            return View(cvm);
        }

        #endregion

        #region Sorting And Filtering

        private void AddSortOrderViewBag(string sortOrder)
        {
            ViewBag.NumberParm = String.IsNullOrEmpty(sortOrder) ? "number_asc" : "";
            ViewBag.NameParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
        }

        private static IList<Account> SortAccounts(IList<Account> accounts, string sortOrder)
        {
            accounts = (sortOrder switch
            {
                "number_asc" => accounts.OrderBy(s => s.Number),
                "name_asc" => accounts.OrderBy(s => s.Name),
                "name_desc" => accounts.OrderByDescending(s => s.Name),
                _ => accounts.OrderByDescending(s => s.Number),
            }).ToList();
            return accounts;
        }

        #endregion
    }
}
