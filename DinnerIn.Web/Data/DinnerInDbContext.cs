using DinnerIn.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Data
{
    public class DinnerInDbContext : DbContext
    {
        public DinnerInDbContext(DbContextOptions<DinnerInDbContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeLike> RecipeLike { get; set; }


    }
}
