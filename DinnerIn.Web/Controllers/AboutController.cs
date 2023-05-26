using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
