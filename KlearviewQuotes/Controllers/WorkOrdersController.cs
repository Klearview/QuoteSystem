using KlearviewQuotes.Models.Clients;
using Microsoft.AspNetCore.Mvc;

namespace KlearviewQuotes.Controllers
{
    public class WorkOrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                "total_dec" => workOrders.OrderByDescending(s => s.SubTotal),
                _ => workOrders.OrderByDescending(s => s.Number)
            }).ToList();
            return workOrders;
        }
    }
}
