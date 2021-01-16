namespace LipitaSystems.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImageService(IDeletableEntityRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task CreateAsync(string imageUrl, int productId)
        {
            var image = new Image
            {
                Url = imageUrl,
                ProductId = productId,
            };

            await this.imageRepository.AddAsync(image);
            await this.imageRepository.SaveChangesAsync();
        }

        public async Task<string> GetProductTumbnailAsync(int productId)
        {
            return await this.imageRepository.AllAsNoTracking()
                .Where(i => i.ProductId == productId)
                .Select(i => i.Url)
                .FirstOrDefaultAsync();
        }

        public string TransformUrlToCropImage(string url)
        {
            var splitUrl = url.Split("upload").ToList();
            splitUrl.Insert(1, "upload/c_fill,h_354,w_500");
            return string.Join(string.Empty, splitUrl);
        }
    }
}
