using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinnerIn.Web.Models.ViewModels
{
    public class AddRecipeRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ServingSuggestions { get; set; }
        public string Chef { get; set; }
        public bool Visible { get; set; }

        //Display tags 
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Collect tags 
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
