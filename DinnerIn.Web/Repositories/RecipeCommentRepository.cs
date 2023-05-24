using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Repositories
{
    public class RecipeCommentRepository : IRecipeCommentRepository
    {
        private readonly DinnerInDbContext dinnerInDbContext;

        public RecipeCommentRepository(DinnerInDbContext dinnerInDbContext)
        {
            this.dinnerInDbContext = dinnerInDbContext;
        }

        public async Task<RecipeComment> AddAsync(RecipeComment recipeComment)
        {
            await dinnerInDbContext.RecipeComment.AddAsync(recipeComment);
            await dinnerInDbContext.SaveChangesAsync(); 
            return recipeComment;
        }
    }
}
