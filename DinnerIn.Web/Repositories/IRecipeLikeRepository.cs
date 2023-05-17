using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Repositories
{
    public interface IRecipeLikeRepository
    {

        Task<int>GetTotalLikes(Guid recipeId);

        Task<IEnumerable<RecipeLike>> GetLikesForRecipe(Guid recipeId);

        Task<RecipeLike> AddLikeForRecipe(RecipeLike recipeLike);
    }
}
