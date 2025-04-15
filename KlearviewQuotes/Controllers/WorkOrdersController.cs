using KlearviewQuotes.Models.Clients;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace KlearviewQuotes.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly IAppDataRepository _repository;

        private static readonly int pageSize = 20;

        public WorkOrdersController(IAppDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string searchString, string accountType, int? workOrderPage, string woSort)
        {
            ClientsController.AddAccountTypeViewBag(ViewBag);
            AddSortOrderViewBag(ViewBag, woSort);

            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentAccountType = accountType;

            var workOrders = await _repository.GetWorkOrdersAsync();

            if (workOrders == null)
                return NotFound();

            if (!string.IsNullOrEmpty(accountType))
                workOrders = workOrders.Where(e => e.Account.AccountType != null && e.Account.AccountType.Contains(accountType)).ToList();

            if (!string.IsNullOrEmpty(searchString))
                workOrders = workOrders.Where(e => e.Account.Contains(searchString)).ToList();

            workOrders = SortWorkOrders(workOrders, woSort);

            return View(workOrders.ToPagedList(workOrderPage ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var workOrder = await _repository.GetWorkOrderAsync(id);

            if (workOrder == null)
                return NotFound();

            return View(workOrder);
        }

        public static void AddSortOrderViewBag(dynamic viewBag, string sortOrder)
        {
            viewBag.WoNumberParm = String.IsNullOrEmpty(sortOrder) ? "number_asc" : "";
            viewBag.WoSubTotalParm = sortOrder == "total_asc" ? "total_desc" : "total_asc";
        }

        public static IList<WorkOrder> SortWorkOrders(IList<WorkOrder> workOrders, string sortOrder)
        {
            workOrders = (sortOrder switch
            {
                "number_asc" => workOrders.OrderBy(s => s.Number),
                "total_asc" => workOrders.OrderBy(s => s.SubTotal),
                "total_desc" => workOrders.OrderByDescending(s => s.SubTotal),
                _ => workOrders.OrderByDescending(s => s.Number)
            }).ToList();
            return workOrders;
        }
    }
}
