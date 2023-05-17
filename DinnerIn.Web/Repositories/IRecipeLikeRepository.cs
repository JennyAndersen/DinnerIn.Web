namespace DinnerIn.Web.Repositories
{
    public interface IRecipeLikeRepository
    {

        Task<int>GetTotalLikes(Guid recipeId); 
    }
}
