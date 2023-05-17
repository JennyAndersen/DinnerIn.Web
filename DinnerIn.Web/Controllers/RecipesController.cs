using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeLikeRepository recipeLikeRepository;

        public RecipesController(IRecipeRepository recipeRepository,
            IRecipeLikeRepository recipeLikeRepository)
        {
            this.recipeRepository = recipeRepository;
            this.recipeLikeRepository = recipeLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var recipe = await recipeRepository.GetByUrlHandleAsync(urlHandle);
            var recipeDetailsViewModel = new RecipeDetailsViewModel();
            

            if(recipe != null)
            {
                var totalLikes = await recipeLikeRepository.GetTotalLikes(recipe.Id);
                
                
                recipeDetailsViewModel = new RecipeDetailsViewModel
                {
                    Id = recipe.Id,
                    Content = recipe.Content,
                    PageTitle = recipe.PageTitle,
                    Chef = recipe.Chef,
                    FeatureImageUrl = recipe.FeatureImageUrl,
                    Heading = recipe.Heading,
                    PublishedDate = recipe.PublishedDate,
                    ShortDescription = recipe.ShortDescription,
                    UrlHandle = recipe.UrlHandle,
                    Visible = recipe.Visible,
                    Tags = recipe.Tags,
                    TotalLikes = totalLikes
                };
            }

            return View(recipeDetailsViewModel);
        }
    }
}
