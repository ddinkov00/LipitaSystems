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

        public string GetProductTumbnail(int productId)
        {
            return this.imageRepository.AllAsNoTracking()
                .Where(i => i.ProductId == productId)
                .Select(i => i.Url)
                .FirstOrDefault();
        }

        public string TransformUrlToCropImage(string url)
        {
            var splitUrl = url.Split("upload");
            return "";
        }
    }
}
