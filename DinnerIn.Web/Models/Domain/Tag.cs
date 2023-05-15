namespace DinnerIn.Web.Models.Domain
{
    public class Tag
    {
        //Lägger till attribut till tag 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
