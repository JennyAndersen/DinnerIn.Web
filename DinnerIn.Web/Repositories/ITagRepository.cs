using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        //single tag back 
        Task<Tag?> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
