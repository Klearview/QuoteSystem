using KlearviewQuotes.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlearviewQuotes.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public async Task<IActionResult> AddRoles()
        {
            foreach (var role in _roles)
            {
                try
                {
                    var result = await _roleManager.CreateAsync(role);

                    if (!result.Succeeded)
                        Errors(result);
                } 
                catch (Exception ex)
                {
                    
                }            
            }       

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var usersInGroup = new Dictionary<IdentityUser, bool>();

            foreach (var user in _userManager.Users)
            {
                var inGroup = await _userManager.IsInRoleAsync(user, role.Name);
                usersInGroup.Add(user, inGroup);
            }

            return View(new RoleEdit
            {
                Role = role,
                UsersInGroup = usersInGroup
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModification model)
        {
            /*IdentityResult result;
            if (true)
            {
                foreach (var userId in model.AddIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await EditRole(model.RoleId);*/

            return RedirectToAction(nameof(Index));
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        private static readonly List<IdentityRole> _roles = new List<IdentityRole>()
        {
            new("Admin"),
            new("QuoteEditor"),
            new("Test")
        };
    }
}
