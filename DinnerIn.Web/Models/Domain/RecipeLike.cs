namespace DinnerIn.Web.Models.Domain
{
    public class RecipeLike
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }

    }
}
