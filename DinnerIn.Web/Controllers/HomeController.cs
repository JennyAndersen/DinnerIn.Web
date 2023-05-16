using DinnerIn.Web.Models;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DinnerIn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository recipeRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, 
            IRecipeRepository recipeRepository,
            ITagRepository tagRepository
            )
        {
            _logger = logger;
            this.recipeRepository = recipeRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await recipeRepository.GetAllAsync();
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                Recipes = recipes,
                Tags = tags
            };

            return View(model);
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