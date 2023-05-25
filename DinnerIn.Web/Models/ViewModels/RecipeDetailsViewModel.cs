using DinnerIn.Web.Models.Domain;

namespace DinnerIn.Web.Models.ViewModels
{
    public class RecipeDetailsViewModel
    {
        //Lägger till attribut till Recept
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ServingSuggestions { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Chef { get; set; }
        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public int TotalLikes { get; set; }

        public bool Liked { get; set; }
        public string CommentDescription { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

    }
}
