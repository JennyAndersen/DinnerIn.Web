using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class AboutController : Controller
    {
        // En kontrollerklass som ärver från Controller-bas-klassen.
        public IActionResult Index()
        {
            // En metod som heter "Index" som är ansvarig för att hantera begäran till "About/Index"-routen.

            return View();
            // Returnerar en vyresultat för att visa en vy som matchar metoden (i det här fallet "Index").
        }
    }
}
