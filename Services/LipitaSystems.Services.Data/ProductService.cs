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

    public class ProductService : IProductService
    {
        private readonly IDiscountCodeService discountCodeService;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IImageService imageService;

        public ProductService(
            IDiscountCodeService discountCodeService,
            IDeletableEntityRepository<Product> productRepository,
            IImageService imageService
            )
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

        public IEnumerable<ProductInListViewModel> DiscountProductsForPaging(int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> DiscountProductsPriceAscendingForPaging(int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> DiscountProductsPriceDescendingForPaging(int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> DiscountProductsQuantityAscendingForPaging(int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> DiscountProductsQuantityDescendingForPaging(int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
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
                    SecondaryCategoryName = p.Category.Name,
                    OriginalPrice = p.OriginalPrice,
                    DiscountPercentage = p.DiscountPercentage,
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> GetAllByCategoryPriceAscendingForPaging(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> GetAllByCategoryPriceDescendingForPaging(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> GetAllByCategoryQuantityAscendingForPaging(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> GetAllByCategoryQuantityDescendingForPaging(int secondaryCategoryId, int page, int itemsPerPage)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public int GetAllCountByCategory(int categoryid)
        {
            return this.productRepository.AllAsNoTracking()
                .Where(p => p.CategoryId == categoryid)
                .Count();
        }

        public ProductByIdViewModel GetById(int productId)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).FirstOrDefault();
        }

        public int GetCountByDiscount()
        {
            return this.productRepository.AllAsNoTracking()
                .Where(p => p.DiscountPercentage != null)
                .Count();
        }

        public int GetCountBySearch(string search)
        {
            return this.productRepository.AllAsNoTracking()
                .Where(p => p.Name.Contains(search))
                .Count();
        }

        public ProductForCheckoutViewModel GetProductForCheckoutById(int id, int quantity, DiscountCodeWithCategoryIds code)
        {
            var product = this.productRepository.AllAsNoTracking()
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
                    DiscountPercentage = p.DiscountPercentage,
                }).FirstOrDefault();

            if (code != null)
            {
                product.FinalPrice = this.discountCodeService
                .ApplyDiscount(
                    code,
                    product.IsDiscounted,
                    product.SecondaryCategoryId,
                    (decimal)product.SummedPrice);
            }
            else
            {
                product.FinalPrice = product.SummedPrice;
            }

            return product;
        }

        public IEnumerable<CartViewModel> GetProductsForCart(Dictionary<int, int> products)
        {
            var viewModel = new List<CartViewModel>();
            foreach (var key in products.Keys)
            {
                var product = this.GetById(key);
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

        public async Task ReduceQuantityInStock(int id, int boughtQuantity)
        {
            var product = this.productRepository.AllAsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefault();

            product.QuantityInStock -= boughtQuantity;
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<ProductInListViewModel> SearchProductsForPaging(int page, int itemsPerPage, string search)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> SearchProductsPriceAscendingForPaging(int page, int itemsPerPage, string search)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> SearchProductsPriceDescendingForPaging(int page, int itemsPerPage, string search)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> SearchProductsQuantityAscendingForPaging(int page, int itemsPerPage, string search)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }

        public IEnumerable<ProductInListViewModel> SearchProductsQuantityDescendingForPaging(int page, int itemsPerPage, string search)
        {
            return this.productRepository.AllAsNoTracking()
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
                }).ToList();
        }
    }
}
