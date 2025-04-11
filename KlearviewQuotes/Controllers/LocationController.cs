using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KlearviewQuotes.Controllers
{
    public class LocationController : Controller
    {
        private readonly IAppDataRepository _repository;

        public LocationController(IAppDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Service(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var location = await _repository.GetServiceLocationAsync(id);

            if (location == null)
                return NotFound();

            return PartialView("_ServiceLocationModal", location);
        }

        public async Task<IActionResult> Billing(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var location = await _repository.GetBillingLocationAsync(id);

            if (location == null)
                return NotFound();

            return PartialView("_BillingLocationModal", location);
        }
    }
}
