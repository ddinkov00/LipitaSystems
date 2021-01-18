namespace LipitaSystems.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        public async Task<List<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files)
        {
            var filesNames = new List<string>();

            foreach (var file in files)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using var destinationStream = new MemoryStream(destinationImage);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), destinationStream),
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                filesNames.Add(result.Url.AbsoluteUri);
            }

            return filesNames;
        }

        public async Task<string> UploadAsyncSingle(Cloudinary cloudinary, IFormFile file)
        {
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using var destinationStream = new MemoryStream(destinationImage);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), destinationStream),
            };

            var result = await cloudinary.UploadAsync(uploadParams);
            return result.Url.AbsoluteUri;
        }
    }
}
