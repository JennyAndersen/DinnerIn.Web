using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinnerIn.Web.Controllers
{
    public class AdminRecipesController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IRecipeRepository recipeRepository;

        public AdminRecipesController(ITagRepository tagRepository, IRecipeRepository recipeRepository)
        {
            this.tagRepository = tagRepository;
            this.recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from the repository 
            var tags = await tagRepository.GetAllAsync();

            var model = new AddRecipeRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeRequest addRecipeRequest)
        {
            //Map the view model to domain model 
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

            //Map tags from selected tags
            foreach (var selectedTagId in addRecipeRequest.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Mapping tag back to domain model 
            recipe.Tags = selectedTags;

            await recipeRepository.AddAsync(recipe);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repository to get the data 
            var recipes = await recipeRepository.GetAllAsync();


            return View(recipes);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository 
            var recipe = await recipeRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (recipe != null)
            {
                //Map the domain model into the view model 
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


            // pass data to view 

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeRequest editRecipeRequest)
        {
            //map view model back to domain model
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


            //Map tags into domain model  

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


            //submit information to repository to update 
            var updatedRecipe = await recipeRepository.UpdateAsync(recipeDomainModel);

            if (updatedRecipe != null)
            {
                //show success notis
                return RedirectToAction("Edit");
            }

            //show failure error notification 
            return RedirectToAction("Edit");

            //redirect to GET 
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditRecipeRequest editRecipeRequest)
        {
            //Talk to repository to delete this blog and tags 
            var deletedRecipe = await recipeRepository.DeleteAsync(editRecipeRequest.Id);

            if (deletedRecipe != null)
            {
                //show success response notif
                return RedirectToAction("List");
            }

            //show error 

            return RedirectToAction("Edit", new { id = editRecipeRequest.Id });

            //display the reponse 
        }
    }
}
