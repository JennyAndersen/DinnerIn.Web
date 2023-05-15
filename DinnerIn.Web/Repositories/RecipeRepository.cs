using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DinnerInDbContext dinnerInDbContext;

        public RecipeRepository(DinnerInDbContext dinnerInDbContext)
        {
            this.dinnerInDbContext = dinnerInDbContext;
        }
        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            await dinnerInDbContext.AddAsync(recipe);
            await dinnerInDbContext.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> DeleteAsync(Guid id)
        {
            var existingRecipe = await dinnerInDbContext.Recipes.FindAsync(id);
            if (existingRecipe != null)
            {
                dinnerInDbContext.Recipes.Remove(existingRecipe);
                await dinnerInDbContext.SaveChangesAsync();
                return existingRecipe;
            }

            return null;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await dinnerInDbContext.Recipes.Include(x => x.Tags).ToListAsync();
        }

        public async Task<Recipe?> GetAsync(Guid id)
        {
            return await dinnerInDbContext.Recipes.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Recipe?> UpdateAsync(Recipe recipe)
        {
            var existingRecipe = await dinnerInDbContext.Recipes.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == recipe.Id);

            if (existingRecipe != null)
            {
                existingRecipe.Id = recipe.Id;
                existingRecipe.Heading = recipe.Heading;
                existingRecipe.PageTitle = recipe.PageTitle;
                existingRecipe.Content = recipe.Content;
                existingRecipe.ShortDescription = recipe.ShortDescription;
                existingRecipe.Chef = recipe.Chef;
                existingRecipe.FeatureImageUrl = recipe.FeatureImageUrl;
                existingRecipe.UrlHandle = recipe.UrlHandle;
                existingRecipe.Visible = recipe.Visible;
                existingRecipe.ServingSuggestions = recipe.ServingSuggestions;
                existingRecipe.PublishedDate = recipe.PublishedDate;
                existingRecipe.Tags = recipe.Tags;

                await dinnerInDbContext.SaveChangesAsync();
                return existingRecipe;
            }

            return null;
        }
    }
}
