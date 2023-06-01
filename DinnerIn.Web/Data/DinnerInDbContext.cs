using DinnerIn.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Data
{
    public class DinnerInDbContext : DbContext
    {
        public DinnerInDbContext(DbContextOptions<DinnerInDbContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }// Definierar en DbSet-egenskap för entiteten "Recipe".
        public DbSet<Tag> Tags { get; set; } // Definierar en DbSet-egenskap för entiteten "Tag".
        public DbSet<RecipeLike> RecipeLike { get; set; }// Definierar en DbSet-egenskap för entiteten "RecipeLike".
        public DbSet<RecipeComment> RecipeComment { get; set; } // Definierar en DbSet-egenskap för entiteten "RecipeComment".

    }
}
