using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // Visa vyn för att lägga till en ny tagg
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Hantera POST-begäranden för att lägga till en ny tagg
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Validera addTagRequest
            ValidateAddTagRequest(addTagRequest);

            // Om modellens tillstånd är ogiltigt, returnera vyn för
            // att visas igen med valideringsfel
            if (ModelState.IsValid == false)
            {
                return View();
            }

            // Kartlägg addTagRequest till tag domänmodellen
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            // Lägg till taggen i tagRepository
            await tagRepository.AddAsync(tag);

            // Omdirigera till listan av taggar
            return RedirectToAction("List");
        }

        // Visa vyn med listan av taggar
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // Hämta alla taggar från tagRepository
            var tags = await tagRepository.GetAllAsync();

            // Skicka taggarna till vyn
            return View(tags);
        }

        // Visa vyn för att redigera en befintlig tagg
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Hämta taggen med det angivna id:et från tagRepository
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                // Kartlägg taggen till editTagRequest vymodellen
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName

                };

                // Visa vyn för att redigera taggen med editTagRequest som data
                return View(editTagRequest);


            }

            // Om taggen inte hittas, returnera en null-vy
            return View(null);
        }

        // Hantera POST-begäranden för att uppdatera en befintlig tagg
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            // Kartlägg editTagRequest till tag domänmodellen
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            // Uppdatera taggen i tagRepository
            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                // Visa framgångsmeddelande 
            }
            else
            {
                // Visa felmeddelande
            }

            // Omdirigera till redigeringsvyn för taggen
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        // Hantera POST-begäranden för att radera en befintlig tagg
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            // Radera taggen med det angivna id från tagRepository
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // Visa framgångsmeddelande om taggen har raderats
                return RedirectToAction("List");
            }


            // Omdirigera till redigeringsvyn för taggen med det angivna id
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        // Validera addTagRequest för att säkerställa att namnet inte är samma som visningsnamnet
        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
    }
}
