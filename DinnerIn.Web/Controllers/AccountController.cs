using DinnerIn.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DinnerIn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        // Konstruktorn tar två parametrar: en UserManager för hantering av användare och en SignInManager för hantering av inloggning.

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            // Tilldelar parametrarna till motsvarande privata medlemsvariabler.
        }

        [HttpGet]
        public IActionResult Register()
        {
            // En GET-action-metod som hanterar begäran till "Account/Register"-routen.
            return View();

            // Returnerar en vyresultat för att visa en vy som matchar metoden (i det här fallet "Register").

        }

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

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                // Skapar en ny användare med hjälp av userManager genom att använda användardata från registerViewModel
                // och det angivna lösenordet.

                if (identityResult.Succeeded)
                {
                    // Kontrollerar om användarskapandet lyckades.

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
        
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel 
            { 
                ReturnUrl = ReturnUrl 
            };

            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            // Show errors
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    } 
}
