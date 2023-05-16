using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var recipe = await recipeRepository.GetByUrlHandleAsync(urlHandle);
            return View(recipe);
        }
    }
}
