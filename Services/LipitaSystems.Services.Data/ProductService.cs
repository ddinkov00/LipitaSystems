namespace LipitaSystems.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IDiscountCodeService discountCodeService;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IImageService imageService;

        public ProductService(
            IDiscountCodeService discountCodeService,
            IDeletableEntityRepository<Product> productRepository,
            IImageService imageService)
        {
            this.discountCodeService = discountCodeService;
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

        public async Task<IEnumerable<ProductInListViewModel>> DiscountProductsForPagingAsync(int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> DiscountProductsPriceAscendingForPagingAsync(int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .OrderBy(p => p.OriginalPrice - (((decimal)p.DiscountPercentage / 100) * p.OriginalPrice))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> DiscountProductsPriceDescendingForPagingAsync(int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .OrderByDescending(p => p.OriginalPrice - (((decimal)p.DiscountPercentage / 100) * p.OriginalPrice))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> DiscountProductsQuantityAscendingForPagingAsync(int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .OrderBy(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> DiscountProductsQuantityDescendingForPagingAsync(int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .OrderByDescending(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryPriceAscendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .OrderBy(p => p.OriginalPrice)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryPriceDescendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .OrderByDescending(p => p.OriginalPrice)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryQuantityAscendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .OrderBy(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryQuantityDescendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == secondaryCategoryId)
                .OrderByDescending(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<int> GetAllCountByCategoryAsync(int categoryid)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == categoryid)
                .CountAsync();
        }

        public async Task<ProductByIdViewModel> GetByIdAsync(int productId)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Id == productId)
                .Select(p => new ProductByIdViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    OriginalPrice = decimal.Round(p.OriginalPrice, 2, MidpointRounding.AwayFromZero),
                    DiscountPercentage = p.DiscountPercentage,
                    MainCategoryName = p.Category.MainCategory.Name,
                    SecondaryCategoryName = p.Category.Name,
                    SecondaryCategoryId = p.Category.Id,
                    MainCategoryId = p.Category.MainCategory.Id,
                    ImagesUlr = p.Images
                        .Select(i => i.Url),
                    QuantityInStock = p.QuantityInStock,
                }).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountByDiscountAsync()
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .CountAsync();
        }

        public async Task<int> GetCountBySearchAsync(string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .CountAsync();
        }

        public async Task<ProductForCheckoutViewModel> GetProductForCheckoutByIdAsync(int id, int quantity, DiscountCodeWithCategoryIds code)
        {
            var product = await this.productRepository.AllAsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductForCheckoutViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.Images
                        .Select(i => i.Url)
                        .FirstOrDefault(),
                    Quantity = quantity,
                    OriginalPrice = p.OriginalPrice,
                    SecondaryCategoryId = p.CategoryId,
                    SecondaryCategoryName = p.Category.Name,
                    MainCategoryId = p.Category.MainCategoryId,
                    DiscountPercentage = p.DiscountPercentage,
                }).FirstOrDefaultAsync();

            if (code != null)
            {
                product.FinalPrice = this.discountCodeService
                .ApplyDiscount(
                    code,
                    product.IsDiscounted,
                    product.MainCategoryId,
                    (decimal)product.SummedPrice);
            }
            else
            {
                product.FinalPrice = product.SummedPrice;
            }

            return product;
        }

        public async Task<List<CartViewModel>> GetProductsForCart(Dictionary<int, int> products)
        {
            var viewModel = new List<CartViewModel>();
            foreach (var key in products.Keys)
            {
                var product = await this.GetByIdAsync(key);
                viewModel.Add(new CartViewModel
                {
                    Id = product.Id,
                    ImageUrl = product.ImagesUlr.First(),
                    Price = product.PriceAfterDiscout == null ? product.OriginalPrice : (decimal)product.PriceAfterDiscout,
                    Product = product.Name,
                    ProductMaxQuantity = product.QuantityInStock,
                    Quantity = products[key],
                    SubCategory = product.SecondaryCategoryName,
                    SubCategoryId = product.SecondaryCategoryId,
                });
            }

            return viewModel;
        }

        public async Task ReduceQuantityInStockAsync(int id, int boughtQuantity)
        {
            var product = await this.productRepository.All()
                .FirstOrDefaultAsync(p => p.Id == id);

            product.QuantityInStock -= boughtQuantity;
            await this.productRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> SearchProductsForPagingAsync(int page, int itemsPerPage, string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> SearchProductsPriceAscendingForPagingAsync(int page, int itemsPerPage, string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .OrderBy(p => p.OriginalPrice)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> SearchProductsPriceDescendingForPagingAsync(int page, int itemsPerPage, string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .OrderByDescending(p => p.OriginalPrice)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> SearchProductsQuantityAscendingForPagingAsync(int page, int itemsPerPage, string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .OrderBy(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListViewModel>> SearchProductsQuantityDescendingForPagingAsync(int page, int itemsPerPage, string search)
        {
            return await this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .OrderByDescending(p => p.QuantityInStock)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToListAsync();
        }
    }
}
