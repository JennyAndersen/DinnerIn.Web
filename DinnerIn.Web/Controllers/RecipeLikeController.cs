using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinnerIn.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeLikeController : ControllerBase
    {
        private readonly IRecipeLikeRepository recipeLikeRepository;

        public RecipeLikeController(IRecipeLikeRepository recipeLikeRepository)
        {
            this.recipeLikeRepository = recipeLikeRepository;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
                 
            var model = new RecipeLike
            {
                RecipeId = addLikeRequest.RecipeId,
                UserId = addLikeRequest.UserId
            };

            await recipeLikeRepository.AddLikeForRecipe(model);

            return Ok();

           
        }


        [HttpGet]
        [Route("{recipeId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForRecipe([FromRoute] Guid recipeId)
        {
            var totalLikes = await recipeLikeRepository.GetTotalLikes(recipeId);

            return Ok(totalLikes);
        }
    }
}
