namespace DinnerIn.Web.Models.Domain
{
    public class RecipeComment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
