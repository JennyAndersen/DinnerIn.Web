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
        // Konstruktorn tar två parametrar: en UserManager för hantering av användare och en SignInManager för hantering av inloggning.

        // Konstruktor för att injicera UserManager och SignInManager
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            // Tilldelar parametrarna till motsvarande privata medlemsvariabler.
        }

        // GET: Visa registreringsvyn
        [HttpGet]
        public IActionResult Register()
        {
            // En GET-action-metod som hanterar begäran till "Account/Register"-routen.
            return View();

            // Returnerar en vyresultat för att visa en vy som matchar metoden (i det här fallet "Register").

        }

        // POST: Hantera registreringsbegäran
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // En POST-action-metod som hanterar begäran till "Account/Register"-routen när formuläret skickas in.

            if (ModelState.IsValid)
            {
                // Kontrollerar om modellens tillstånd är giltigt, dvs. att valideringen för registerViewModel har passerat.
                var identityUser = new IdentityUser
                {
                    // Skapar en ny instans av IdentityUser med användardata från registerViewModel.
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                // Skapa användaren med hjälp av UserManager
                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                // Skapar en ny användare med hjälp av userManager genom att använda användardata från registerViewModel
                // och det angivna lösenordet.

                if (identityResult.Succeeded)
                {
                    // Kontrollerar om användarskapandet lyckades.

                    // Tilldela användaren rollen "User"
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                    // Lägger till den nyregistrerade användaren till rollen "User".
                    if (roleIdentityResult.Succeeded)
                    {
                        // Kontrollerar om tilldelningen av rollen lyckades.

                        // Visa en framgångsrik notifikation.

                        return RedirectToAction("Register");
                        // Utför en omdirigering till "Register" för att visa registreringsvyn igen.
                    }
                }
            }

            // Om modellens tillstånd inte är giltigt eller någon av de tidigare stegen misslyckades,
            // returnera vyn igen för att visa eventuella felmeddelanden.

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
