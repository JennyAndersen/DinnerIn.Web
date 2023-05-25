using DinnerIn.Web.Models.Domain;
using System.Collections;

namespace DinnerIn.Web.Repositories
{
    public interface IRecipeCommentRepository
    {
        Task<RecipeComment> AddAsync(RecipeComment recipeComment);
        Task<IEnumerable<RecipeComment>> GetCommentsByRecipeAsync(Guid recipeId); 
    }   
}
