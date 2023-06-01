using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace DinnerIn.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;// En referens till IConfiguration som används för att hämta Cloudinary-konfigurationsdata.
        private readonly Account account;// En instans av Account för att hantera Cloudinary-kontoinformationen.

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],// Hämtar Cloudinary CloudName från konfigurationsdata
                configuration.GetSection("Cloudinary")["ApiKey"], // Hämtar Cloudinary ApiKey från konfigurationsdata.
                configuration.GetSection("Cloudinary")["ApiSecret"] // Hämtar Cloudinary ApiSecret från konfigurationsdata.
               );
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);


            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
