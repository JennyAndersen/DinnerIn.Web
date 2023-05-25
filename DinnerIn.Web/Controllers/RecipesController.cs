using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeLikeRepository recipeLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManger;
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
            this.userManger = userManger;
            this.recipeCommentRepository = recipeCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var recipe = await recipeRepository.GetByUrlHandleAsync(urlHandle);
            var recipeDetailsViewModel = new RecipeDetailsViewModel();
            var liked = false; 
            

            if(recipe != null)
            {
                var totalLikes = await recipeLikeRepository.GetTotalLikes(recipe.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //Get like for this blog for this user 
                    var likesForRecipe = await recipeLikeRepository.GetLikesForRecipe(recipe.Id);

                    var userId = userManger.GetUserId(User); 

                    if(userId != null)
                    {
                        var likeFromUser = likesForRecipe.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }

                }

                //Get comments for recipe
                var commentsDomainModel = await recipeCommentRepository.GetCommentsByRecipeAsync(recipe.Id);

                var commentsForView = new List<Comment>(); 

                foreach (var comment in commentsDomainModel)
                {
                    commentsForView.Add(new Comment
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await userManger.FindByIdAsync(comment.UserId.ToString())).UserName
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

                var domainModel = new RecipeComment
                {
                    RecipeId = recipeDetailsViewModel.Id,
                    Description = recipeDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManger.GetUserId(User)),
                    DateAdded = DateTime.Now 
                };


                await recipeCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Recipes", 
                    new {urlHandle = recipeDetailsViewModel.UrlHandle});
            }

            return View(); 
        }
    }
}
