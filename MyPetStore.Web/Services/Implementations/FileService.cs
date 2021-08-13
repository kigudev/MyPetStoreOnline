using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyPetStore.Web.Services.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace MyPetStore.Web.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Guarda la imagen en una ubicación del servidor
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> SaveImageAsync(IFormFile image)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "images/uploads", fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            return Path.Combine("images/uploads", fileName);
        }
    }
}