using DinnerIn.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Repositories
{
    public class RecipeLikeRepository : IRecipeLikeRepository
    {
        private readonly DinnerInDbContext dinnerInDbContext;

        public RecipeLikeRepository(DinnerInDbContext dinnerInDbContext) 
        {
            this.dinnerInDbContext = dinnerInDbContext;
        }

        
        public async Task<int> GetTotalLikes(Guid recipeId)
        {
            return await dinnerInDbContext.RecipeLike
                .CountAsync(x => x.RecipeId == recipeId);
        }

    }
}
