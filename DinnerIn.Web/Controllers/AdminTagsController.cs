﻿using DinnerIn.Web.Data;
using DinnerIn.Web.Models.Domain;
using DinnerIn.Web.Models.ViewModels;
using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //mapping addtag request to tag domain model 
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };


            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName

                };

                return View(editTagRequest);


            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //show success 
            }
            else
            {
                //show error 
            }

            //show error notification 
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success notification 
                return RedirectToAction("List");
            }


            //Show error notifcation 
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
