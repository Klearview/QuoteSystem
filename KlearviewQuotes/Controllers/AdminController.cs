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
            var usersInGroupAlwaysAdmin = new Dictionary<IdentityUser, bool>();
            var usersInGroup = new Dictionary<IdentityUser, bool>();

            foreach (var user in _userManager.Users)
            {
                var inGroup = await _userManager.IsInRoleAsync(user, role.Name);

                if (role.Name == "Admin" && inGroup && await _userManager.IsInRoleAsync(user, "AlwaysAdmin"))
                    usersInGroupAlwaysAdmin.Add(user, inGroup);
                else
                    usersInGroup.Add(user, inGroup);
            }

            return View(new RoleEdit
            {
                Role = role,
                UsersInGroupAlwaysAdmin = usersInGroupAlwaysAdmin,
                UsersInGroup = usersInGroup
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var user in _userManager.Users.ToList())
                    if (await _userManager.IsInRoleAsync(user, model.RoleName))
                        await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                foreach (var userId in model.UsersInGroup ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, model.RoleName);
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await EditRole(model.RoleId);
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
            new("Test"),
            new("AlwaysAdmin")
        };

    }
}
