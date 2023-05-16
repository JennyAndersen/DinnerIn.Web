using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
