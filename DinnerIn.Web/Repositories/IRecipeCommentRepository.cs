using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Repositories
{
    public interface IRecipeCommentRepository
    {
        Task<RecipeComment> AddAsync(RecipeComment recipeComment); 
    }
}
