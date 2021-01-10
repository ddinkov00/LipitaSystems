namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using LipitaSystems.Services;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.Controllers;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : AdministrationController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageService imageService;

        public ProductController(
            ICategoryService categoryService,
            IProductService productService,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            IImageService imageService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var inputModel = new ProductInputModel();
            inputModel.CategoryItems = this.categoryService.GetAllForSelectList();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel inputModel)
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

            return this.RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}
