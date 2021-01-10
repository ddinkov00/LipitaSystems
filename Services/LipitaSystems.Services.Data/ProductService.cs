namespace LipitaSystems.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IImageService imageService;

        public ProductService(
            IDeletableEntityRepository<Product> productRepository,
            IImageService imageService)
        {
            this.productRepository = productRepository;
            this.imageService = imageService;
        }

        public async Task<int> CreateAsync(ProductInputModel inputModel)
        {
            var product = new Product
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                CategoryId = inputModel.SecondaryCategoryId,
                OriginalPrice = inputModel.Price,
                QuantityInStock = inputModel.QuantityInstock,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();

            return product.Id;
        }

        public IEnumerable<ProductInListViewModel> GetAllByCategoryForPaging(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = this.imageService.GetProductTumbnail(p.Id),
                    QuantityInStock = p.QuantityInStock,
                    OriginalPrice = p.OriginalPrice,
                    PriceAfterDiscout = p.PriceAfterDiscount,
                }).ToList();
        }
    }
}
