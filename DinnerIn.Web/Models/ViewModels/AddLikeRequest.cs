namespace DinnerIn.Web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}
