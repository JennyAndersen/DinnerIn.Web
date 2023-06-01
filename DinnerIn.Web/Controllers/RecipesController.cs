using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeLikeRepository recipeLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IRecipeCommentRepository recipeCommentRepository;

        public RecipesController(IRecipeRepository recipeRepository,
            IRecipeLikeRepository recipeLikeRepository,
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManger,
            IRecipeCommentRepository recipeCommentRepository)
        {
            this.recipeRepository = recipeRepository;
            this.recipeLikeRepository = recipeLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManger;
            this.recipeCommentRepository = recipeCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            // Hämta receptet med den angivna urlHandle
            var recipe = await recipeRepository.GetByUrlHandleAsync(urlHandle);
            var recipeDetailsViewModel = new RecipeDetailsViewModel();
            var liked = false; 
            

            if(recipe != null)
            {
                // Hämta totala antalet gillanden för receptet
                var totalLikes = await recipeLikeRepository.GetTotalLikes(recipe.Id);

                if (signInManager.IsSignedIn(User))
                {
                    // Om användaren är inloggad, kontrollera om de har gillat receptet
                    var likesForRecipe = await recipeLikeRepository.GetLikesForRecipe(recipe.Id);

                    var userId = userManager.GetUserId(User); 

                    if(userId != null)
                    {
                        var likeFromUser = likesForRecipe.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }

                }

                // Hämta kommentarer för receptet
                var commentsDomainModel = await recipeCommentRepository.GetCommentsByRecipeAsync(recipe.Id);

                var commentsForView = new List<Comment>(); 

                foreach (var comment in commentsDomainModel)
                {
                    // Skapa en ny Comment-modell för visning
                    commentsForView.Add(new Comment
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    }); 
                }
                
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
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = commentsForView,
                };
            }

            return View(recipeDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(RecipeDetailsViewModel recipeDetailsViewModel)
        {
           if(signInManager.IsSignedIn(User))
            {
                // Skapa en RecipeComment-domänmodell från RecipeDetailsViewModel
                var domainModel = new RecipeComment
                {
                    RecipeId = recipeDetailsViewModel.Id,
                    Description = recipeDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now 
                };

                // Lägg till kommentaren i repository
                await recipeCommentRepository.AddAsync(domainModel);

                // Omdirigera tillbaka till Index-åtgärden för att uppdatera sidan
                return RedirectToAction("Index", "Recipes", 
                    new {urlHandle = recipeDetailsViewModel.UrlHandle});
            }

            return View(); 
        }
    }
}
