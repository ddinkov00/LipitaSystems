namespace LipitaSystems.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using LipitaSystems.Services;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageService imageService;

        public ShopController(
            IProductService productService,
            ICategoryService categoryService,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            IImageService imageService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
        }

        public IActionResult All()
        {
            var categories = this.categoryService.GetAllForSelectList();
            return this.View(categories);
        }

        public IActionResult AllSubCategories(int id)
        {
            var subCategories = this.categoryService.GetAllSubCategoriesForSelectList(id);
            return this.View(subCategories);
        }

        public IActionResult Products(int secondaryCategoryId, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;

            var viewModel = new ProductListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.productService.GetAllCountByCategory(secondaryCategoryId),
                Products = this.productService.GetAllByCategoryForPaging(secondaryCategoryId, id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Product()
        {
            return this.View();
        }

        public IActionResult AddProduct()
        {
            var inputModel = new ProductInputModel();
            inputModel.CategoryItems = this.categoryService.GetAllForSelectList();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var productId = await this.productService.CreateAsync(inputModel);
            var imageUrls = await this.cloudinaryService.UploadAsync(this.cloudinary, inputModel.Images.ToList());

            foreach (var imageUrl in imageUrls)
            {
                await this.imageService.CreateAsync(imageUrl, productId);
            }

            return this.Redirect("/");
        }
    }
}
