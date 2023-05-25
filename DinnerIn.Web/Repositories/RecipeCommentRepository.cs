using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<RecipeComment>> GetCommentsByRecipeAsync(Guid recipeId)
        {
           return await dinnerInDbContext.RecipeComment.Where(x => x.RecipeId == recipeId).ToListAsync();
        }
    }
}
