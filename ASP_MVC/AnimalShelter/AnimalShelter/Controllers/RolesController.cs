using AnimalShelter.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AnimalShelter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Przypisanie roli do użytkownika (zastąpienie istniejącej roli)
        public IActionResult AssignRole()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList(); // Pobierz role z bazy danych

            var userRoles = users.ToDictionary(
                user => user.Id,
                user => _userManager.GetRolesAsync(user).Result.ToList()
            );

            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;
            return View(users);
        }

        [HttpPost]
        public IActionResult AssignRole(string userId, string role)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null || string.IsNullOrEmpty(role))
            {
                return RedirectToAction(nameof(AssignRole));
            }

            // Get the current roles of the user and remove them using LINQ
            var currentRoles = _userManager.GetRolesAsync(user).Result;
            var removeRolesResult = currentRoles.Select(currentRole =>
                _userManager.RemoveFromRoleAsync(user, currentRole).Result
            ).All(result => result.Succeeded);

            // Check if all role removals succeeded
            if (removeRolesResult)
            {
                var addRoleResult = _userManager.AddToRoleAsync(user, role).Result;
                if (addRoleResult.Succeeded)
                {
                    return RedirectToAction(nameof(AssignRole));
                }

                // Handle any errors (optional)
                foreach (var error in addRoleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(AssignRole));
        }
    }
}
