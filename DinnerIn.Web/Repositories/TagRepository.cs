using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace DinnerIn.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DinnerInDbContext dinnerInDbContext;

        public TagRepository(DinnerInDbContext dinnerInDbContext)
        {
            this.dinnerInDbContext = dinnerInDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            //Skriver till databas. 
            await dinnerInDbContext.Tags.AddAsync(tag);
            await dinnerInDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dinnerInDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                dinnerInDbContext.Tags.Remove(existingTag);
                await dinnerInDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dinnerInDbContext.Tags.ToListAsync();
     
        }


        public Task<Tag?> GetAsync(Guid id)
        {
            return dinnerInDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dinnerInDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await dinnerInDbContext.SaveChangesAsync();

                return existingTag;
                
            }

            return null;
        }
    }
}
