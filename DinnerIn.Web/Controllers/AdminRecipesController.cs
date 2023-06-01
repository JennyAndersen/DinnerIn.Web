using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinnerIn.Web.Controllers
{
    //endast användare med rollen "Admin" har åtkomst till denna kontrollerklass.
    [Authorize(Roles = "Admin")]
    public class AdminRecipesController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IRecipeRepository recipeRepository;

        // Konstruktor för att injicera TagRepository och RecipeRepository
        public AdminRecipesController(ITagRepository tagRepository, IRecipeRepository recipeRepository)
        {
            this.tagRepository = tagRepository;
            this.recipeRepository = recipeRepository;
        }

        // GET: Visa vyn för att lägga till ett nytt recept
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Hämta taggar från tagRepository
            var tags = await tagRepository.GetAllAsync();

            // Skapa en AddRecipeRequest-modell och tilldela 
            //taggarna till SelectListItem-listan i modellen
            var model = new AddRecipeRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        // POST: Hantera begäran för att lägga till ett nytt recept
        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeRequest addRecipeRequest)
        {
            // Kartlägg vymodellen till domänmodellen
            var recipe = new Recipe
            {
                Heading = addRecipeRequest.Heading,
                PageTitle = addRecipeRequest.PageTitle,
                Content = addRecipeRequest.Content,
                ShortDescription = addRecipeRequest.ShortDescription,
                FeatureImageUrl = addRecipeRequest.FeatureImageUrl,
                UrlHandle = addRecipeRequest.UrlHandle,
                PublishedDate = addRecipeRequest.PublishedDate,
                ServingSuggestions = addRecipeRequest.ServingSuggestions,
                Chef = addRecipeRequest.Chef,
                Visible = addRecipeRequest.Visible,

            };

            var selectedTags = new List<Tag>();

            // Kartlägg valda taggar från vymodellen
            foreach (var selectedTagId in addRecipeRequest.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            // Kartlägg taggarna tillbaka till domänmodellen
            recipe.Tags = selectedTags;

            // Lägg till receptet med recipeRepository
            await recipeRepository.AddAsync(recipe);

            //// Omdirigera till "Add"-åtgärden
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Anropa repository för att hämta data
            var recipes = await recipeRepository.GetAllAsync();

            // Skicka data till vyn
            return View(recipes);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            // Hämta resultatet från repository
            var recipe = await recipeRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (recipe != null)
            {
                
                // Kartlägg domänmodellen till vymodellen
                var model = new EditRecipeRequest
                {
                    Id = recipe.Id,
                    Heading = recipe.Heading,
                    PageTitle = recipe.PageTitle,
                    Content = recipe.Content,
                    Chef = recipe.Chef,
                    ServingSuggestions = recipe.ServingSuggestions,
                    FeatureImageUrl = recipe.FeatureImageUrl,
                    UrlHandle = recipe.UrlHandle,
                    ShortDescription = recipe.ShortDescription,
                    PublishedDate = recipe.PublishedDate,
                    Visible = recipe.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),
                    SelectedTags = recipe.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);

            }


            // Skicka null till vyn om receptet inte hittades
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeRequest editRecipeRequest)
        {
            // Kartlägg vymodellen tillbaka till domänmodellen
            var recipeDomainModel = new Recipe
            {
                Id = editRecipeRequest.Id,
                Heading = editRecipeRequest.Heading,
                PageTitle = editRecipeRequest.PageTitle,
                Content = editRecipeRequest.Content,
                Chef = editRecipeRequest.Chef,
                ServingSuggestions = editRecipeRequest.ServingSuggestions,
                ShortDescription = editRecipeRequest.ShortDescription,
                FeatureImageUrl = editRecipeRequest.FeatureImageUrl,
                PublishedDate = editRecipeRequest.PublishedDate,
                UrlHandle = editRecipeRequest.UrlHandle,
                Visible = editRecipeRequest.Visible,
            };


            // Kartlägg taggar tillbaka till domänmodellen
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editRecipeRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            recipeDomainModel.Tags = selectedTags;


            // Skicka informationen till repository för att uppdatera
            var updatedRecipe = await recipeRepository.UpdateAsync(recipeDomainModel);

            if (updatedRecipe != null)
            {
                // Visa en framgångsrik notifiering
                return RedirectToAction("Edit");
            }

            // Visa en felnotifiering vid misslyckande
            return RedirectToAction("Edit");

            // Omdirigera till GET
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditRecipeRequest editRecipeRequest)
        {
            // Radera receptet och dess taggar genom att prata med repository
            var deletedRecipe = await recipeRepository.DeleteAsync(editRecipeRequest.Id);

            if (deletedRecipe != null)
            {
                // Visa en framgångsrik notifiering
                return RedirectToAction("List");
            }

            // Visa ett felmeddelande vid misslyckande
            return RedirectToAction("Edit", new { id = editRecipeRequest.Id });

            
        }
    }
}
