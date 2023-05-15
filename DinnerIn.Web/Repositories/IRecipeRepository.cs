using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetAsync(Guid id);
        Task<Recipe> AddAsync(Recipe recipe);
        Task<Recipe?> UpdateAsync(Recipe recipe);
        Task<Recipe?> DeleteAsync(Guid id);


    }
}
