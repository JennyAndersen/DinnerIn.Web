using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DinnerIn.Web.Models.ViewModels
{
    public class AddRecipeRequest
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string PageTitle { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FeatureImageUrl { get; set; }
       
        public string? UrlHandle { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string ServingSuggestions { get; set; }
        [Required]
        public string Chef { get; set; }
        [Required]
        public bool Visible { get; set; }

        //Display tags 
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Collect tags 
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
