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
            // Skapa en RecipeLike-modell med data från AddLikeRequest     
            var model = new RecipeLike
            {
                RecipeId = addLikeRequest.RecipeId,
                UserId = addLikeRequest.UserId
            };

            // Anropa recipeLikeRepository för att lägga till ett gillande för receptet
            await recipeLikeRepository.AddLikeForRecipe(model);

            // Returnera ett Ok-resultat för att signalera att operationen lyckades
            return Ok();

           
        }


        [HttpGet]
        [Route("{recipeId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForRecipe([FromRoute] Guid recipeId)
        {
            // Anropa recipeLikeRepository för att hämta totala antalet gillanden för receptet
            var totalLikes = await recipeLikeRepository.GetTotalLikes(recipeId);

            // Returnera ett Ok-resultat med det totala antalet gillanden
            return Ok(totalLikes);
        }
    }
}
