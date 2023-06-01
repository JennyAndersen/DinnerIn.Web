using DinnerIn.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DinnerIn.Web.Controllers
{
    //Kontrollerklass för hantering av användarkonton 
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        // Konstruktor för att injicera UserManager och SignInManager
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Visa registreringsvyn
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Hantera registreringsbegäran
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // Kontrollera om modellen är giltig
            if (ModelState.IsValid)
            {

                // Skapa en ny IdentityUser med användarnamn och e-postadress från vyn
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                // Skapa användaren med hjälp av UserManager
                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {

                    // Tilldela användaren rollen "User"
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        // Visa en framgångsnotifikation och omdirigera till registreringsvyn 
                        return RedirectToAction("Register");
                    }
                }
            }

            // Visa registreringsvyn igen om något gick fel
            return View();
        }


        // GET: Visa inloggningsvyn
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel 
            { 
                ReturnUrl = ReturnUrl 
            };

            return View(model); 
        }

        // POST: Hantera inloggningsbegäran
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            // Kontrollera om modellen är giltig
            if (!ModelState.IsValid)
            {
                // Visa inloggningsvyn igen om modellen inte är giltig
                return View();
            }

            // Försök att logga in användaren med hjälp av SignInManager
            // och det angivna användarnamnet och lösenordet
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                // Kontrollera om det finns en ReturnUrl och omdirigera till den
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                // Annars omdirigera till startsidan
                return RedirectToAction("Index", "Home");
            }

            // Visa inloggningsvyn igen om inloggningen misslyckades
            return View();

        }

        // GET: Logga ut användaren
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Logga ut användaren med hjälp av SignInManager
            await signInManager.SignOutAsync();

            // Omdirigera till startsidan
            return RedirectToAction("Index", "Home");
        }

        // GET: Visa åtkomst nekad-vyn
        [HttpGet]
        public IActionResult AccessDenied()
        {
            // Visa åtkomst nekad-vyn
            return View();
        }

    } 
}
