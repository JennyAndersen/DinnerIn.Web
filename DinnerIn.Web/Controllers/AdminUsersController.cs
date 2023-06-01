using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DinnerIn.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository,
            UserManager<IdentityUser> userManager) 
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        // Hantera GET-begäranden för att lista användare
        [HttpGet]
        public async Task<IActionResult> List(string searchString)
        {
            // Hämta alla användare från userRepository
            var users = await userRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Filtrera användare baserat på söksträngen (användarnamn eller e-post)
                users = users.Where(u => u.UserName.Contains(searchString) || u.Email.Contains(searchString));
            }

            // Skapa en UserViewModel för att visa användardata i vyn
            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                // Konvertera varje User-objekt till en User i modellens vy för att visa relevanta egenskaper
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    EmailAddress = user.Email
                });
            }

            // Returnera vyn med användaruppgifterna
            return View(usersViewModel);
        }

        // Hantera POST-begäranden för att lägga till en ny användare
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            // Skapa en IdentityUser baserat på användarens inmatning i vyn
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            // Skapa användaren i UserManager med hjälp av identityUser och lösenordet från request
            var identityResult =
                await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // Tilldela roller till denna användare
                    var roles = new List<string> { "User" };

                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    // Lägg till användaren till de angivna rollerna
                    identityResult =
                        await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        // Vid lyckad skapande av användare och tilldelning av roller, omdirigera till användarlistan
                        return RedirectToAction("List", "AdminUsers");
                    }

                }
            }

            // Visa vyn igen vid fel eller misslyckande
            return View();
        }

        // Hantera POST-begäranden för att radera en användare baserat på id
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Hämta användaren med det angivna id från UserManager
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                // Radera användaren med hjälp av UserManager
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    // Vid lyckad radering av användare, omdirigera till användarlistan
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            // Visa vyn igen vid fel eller misslyckande
            return View();
        }


    }
}
