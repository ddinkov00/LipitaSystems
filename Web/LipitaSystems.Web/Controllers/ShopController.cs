namespace LipitaSystems.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using LipitaSystems.Services;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
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

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = this.imageService.GetProductTumbnail(product.Id);
            }

            return this.View(viewModel);
        }

        public IActionResult Product()
        {
            return this.View();
        }

        [HttpGet]
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

        public IActionResult Cart()
        {
            var viewModel = new List<CartViewModel>();

            if (this.HttpContext.Request.Cookies.ContainsKey("cartProduct"))
            {
                var cookies = this.HttpContext.Request.Cookies["cartProduct"].Split('_');
                var products = new Dictionary<int, int>();
                for (int i = 0; i < cookies.Length; i += 2)
                {
                    if (!products.ContainsKey(int.Parse(cookies[i])))
                    {
                        products.Add(int.Parse(cookies[i]), 0);
                    }

                    products[int.Parse(cookies[i])] += int.Parse(cookies[i + 1]);
                }
            }

            return this.View(viewModel);
        }
    }
}
