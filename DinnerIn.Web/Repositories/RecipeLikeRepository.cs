using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
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

        
        public async Task<RecipeLike> AddLikeForRecipe(RecipeLike recipeLike)
        {
            await dinnerInDbContext.RecipeLike.AddAsync(recipeLike);
            await dinnerInDbContext.SaveChangesAsync();
            return recipeLike;

        }
 

        public async Task<IEnumerable<RecipeLike>> GetLikesForRecipe(Guid recipeId)
        {
            return await dinnerInDbContext.RecipeLike.Where(x => x.RecipeId == recipeId)
                .ToListAsync();
        }


        public async Task<int> GetTotalLikes(Guid recipeId)
        {
            return await dinnerInDbContext.RecipeLike
                .CountAsync(x => x.RecipeId == recipeId);
        }

    }
}
