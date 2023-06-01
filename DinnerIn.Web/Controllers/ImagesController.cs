using DinnerIn.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace DinnerIn.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {


        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // Anropa imageRepository för att ladda upp filen
            var imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                // Om laddningen misslyckas, returnera ett Problem-resultat med en felmeddelande och statuskod
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            // Returnera en JsonResult med URL-länken till den uppladdade bilden
            return new JsonResult(new { link = imageURL });
        }

    }
}
