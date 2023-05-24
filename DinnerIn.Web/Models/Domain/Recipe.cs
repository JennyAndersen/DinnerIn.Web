namespace DinnerIn.Web.Models.Domain
{
    public class Recipe
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

        //Navigation property 
        public ICollection<Tag> Tags { get; set; }

        public ICollection<RecipeLike> Likes { get; set; }
        public ICollection<RecipeComment> Comments { get; set; }

        
    }
}
