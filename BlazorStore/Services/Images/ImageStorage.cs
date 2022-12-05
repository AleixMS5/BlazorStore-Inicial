using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace BlazorStore.Services.Images
{
    public class ImageStorage : IImageStorage
    {
        private readonly IWebHostEnvironment _environment;

        public ImageStorage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveImageAsync(string name, Stream stream)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "images", "uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var imagePath = Path.Combine(uploadFolder, name);
            var path = Path.Combine(_environment.WebRootPath, imagePath);
            await using var fileStream = File.Create(path);
            await stream.CopyToAsync(fileStream, CancellationToken.None);
            return imagePath;
        }

        public Task RemoveImageAsync(string name)
        {
            var path = Path.Combine(_environment.WebRootPath, "images", "uploads", name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return Task.CompletedTask;
        }
    }
}