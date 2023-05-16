using DinnerIn.Web.Models;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DinnerIn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository recipeRepository;

        public HomeController(ILogger<HomeController> logger, IRecipeRepository recipeRepository)
        {
            _logger = logger;
            this.recipeRepository = recipeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await recipeRepository.GetAllAsync();

            return View(recipes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}