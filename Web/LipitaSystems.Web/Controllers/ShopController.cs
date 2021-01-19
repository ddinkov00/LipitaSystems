namespace LipitaSystems.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using CloudinaryDotNet;
    using LipitaSystems.Services;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class ShopController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageService imageService;
        private readonly IDiscountCodeService discountCodeService;
        private readonly IOrderService orderService;
        private readonly IDeliveryOfficeService deliveryOfficeService;

        public ShopController(
            IProductService productService,
            ICategoryService categoryService,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            IImageService imageService,
            IDiscountCodeService discountCodeService,
            IOrderService orderService,
            IDeliveryOfficeService deliveryOfficeService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
            this.discountCodeService = discountCodeService;
            this.orderService = orderService;
            this.deliveryOfficeService = deliveryOfficeService;
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.categoryService.GetAllForSelectListAsync();
            return this.View(categories);
        }

        public async Task<IActionResult> AllSubCategories(int id)
        {
            var subCategories = await this.categoryService.GetAllSubCategoriesForSelectListAsync(id);
            return this.View(subCategories);
        }

        public async Task<IActionResult> Products(int id, string order, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            const int itemsPerPage = 3;
            var subCategory = await this.categoryService.GetSubCategoryNameByIdAsync(id);
            var category = await this.categoryService.GetCategoryNameByIdAsync(subCategory.MainCategoryId);
            var viewModel = new ProductListViewModel
            {
                Category = category,
                Id = subCategory.MainCategoryId,
                SubCategory = subCategory.Name,
                SubCategoryId = id,
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = await this.productService.GetAllCountByCategoryAsync(id),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = await this.productService.GetAllByCategoryPriceAscendingForPagingAsync(id, page, itemsPerPage);
                    break;
                case "PriceDescending":
                    viewModel.Products = await this.productService.GetAllByCategoryPriceDescendingForPagingAsync(id, page, itemsPerPage);
                    break;
                case "QuantityAscending":
                    viewModel.Products = await this.productService.GetAllByCategoryQuantityAscendingForPagingAsync(id, page, itemsPerPage);
                    break;
                case "QuantityDescending":
                    viewModel.Products = await this.productService.GetAllByCategoryQuantityDescendingForPagingAsync(id, page, itemsPerPage);
                    break;
                default:
                    viewModel.Products = await this.productService.GetAllByCategoryForPagingAsync(id, page, itemsPerPage);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = await this.imageService.GetProductTumbnailAsync(product.Id);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Product(int id)
        {
            var viewModel = await this.productService.GetByIdAsync(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Cart(bool? isCodeValid)
        {
            var viewModel = new CartListViewModel();
            viewModel.Products = new List<CartViewModel>();

            if (isCodeValid != null)
            {
                if ((bool)!isCodeValid)
                {
                    viewModel.IsCodeValid = isCodeValid;
                }
            }

            if (this.HttpContext.Request.Cookies.ContainsKey("cartProducts"))
            {
                var cookies = this.HttpContext.Request.Cookies["cartProducts"].Split('_');
                if (cookies[0] != string.Empty)
                {
                    var products = new Dictionary<int, int>();
                    for (int i = 0; i < cookies.Length; i += 2)
                    {
                        if (!products.ContainsKey(int.Parse(cookies[i])))
                        {
                            products.Add(int.Parse(cookies[i]), 0);
                        }

                        products[int.Parse(cookies[i])] += int.Parse(cookies[i + 1]);
                    }

                    viewModel.Products = await this.productService.GetProductsForCart(products);
                }
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Cart(string discountCode)
        {
            if (discountCode != null)
            {
                var isCodeValid = await this.discountCodeService.IsDiscountCodeValid(discountCode);

                if (!isCodeValid)
                {
                    return this.RedirectToAction(nameof(this.Cart), new { isCodeValid = isCodeValid });
                }
            }

            return this.RedirectToAction(nameof(this.Checkout), new { discountCode = discountCode });
        }

        public async Task<IActionResult> Checkout(string discountCode)
        {
            var cookies = this.HttpContext.Request.Cookies["cartProducts"] != null 
                ? this.HttpContext.Request.Cookies["cartProducts"].Split('_')
                : null;

            var viewModel = new ProductListForCashOutInputModel();

            if (cookies.Any())
            {
                var code = await this.discountCodeService.GetDiscountCodeAsync(discountCode);
                viewModel.DiscountCode = code;

                for (int i = 0; i < cookies.Length; i += 2)
                {
                    var productId = int.Parse(cookies[i]);
                    var productQuantity = int.Parse(cookies[i + 1]);

                    viewModel.Products.Add(await this.productService
                        .GetProductForCheckoutByIdAsync(productId, productQuantity, code));
                }
            }

            viewModel.DeliveryOfficeItems = await this.deliveryOfficeService.GetAllForSelectListAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ProductListForCashOutInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var cookiees = this.HttpContext.Request.Cookies["cartProducts"].Split('_');

                if (cookiees.Any())
                {
                    var code = input.DiscountCodeName == null ? null : await this.discountCodeService.GetDiscountCodeAsync(input.DiscountCode.DiscountName);
                    input.DiscountCode = code;

                    for (int i = 0; i < cookiees.Length; i += 2)
                    {
                        var productId = int.Parse(cookiees[i]);
                        var productQuantity = int.Parse(cookiees[i + 1]);

                        input.Products.Add(await this.productService
                            .GetProductForCheckoutByIdAsync(productId, productQuantity, code));
                    }
                }

                input.DeliveryOfficeItems = await this.deliveryOfficeService.GetAllForSelectListAsync();
                return this.View(input);
            }

            var cookies = this.HttpContext.Request.Cookies["cartProducts"].Split('_');

            if (cookies.Any())
            {
                for (int i = 0; i < cookies.Length; i += 2)
                {
                    var productId = int.Parse(cookies[i]);
                    var productQuantity = int.Parse(cookies[i + 1]);

                    await this.productService.ReduceQuantityInStockAsync(productId, productQuantity);

                    var code = input.DiscountCodeName == null ? null : await this.discountCodeService.GetDiscountCodeAsync(input.DiscountCodeName);

                    input.Products.Add(await this.productService
                        .GetProductForCheckoutByIdAsync(productId, productQuantity, code));
                }
            }

            await this.orderService.MakeOrder(input);
            this.Response.Cookies.Delete("cartProducts", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1),
                Secure = true,
            });
            return this.Redirect("/");
        }

        public async Task<IActionResult> Discount(string order, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            const int itemsPerPage = 3;
            var viewModel = new ProductListViewModel
            {
                Search = "Намалени продукти",
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = await this.productService.GetCountByDiscountAsync(),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = await this.productService.DiscountProductsPriceAscendingForPagingAsync(page, itemsPerPage);
                    break;
                case "PriceDescending":
                    viewModel.Products = await this.productService.DiscountProductsPriceDescendingForPagingAsync(page, itemsPerPage);
                    break;
                case "QuantityAscending":
                    viewModel.Products = await this.productService.DiscountProductsQuantityAscendingForPagingAsync(page, itemsPerPage);
                    break;
                case "QuantityDescending":
                    viewModel.Products = await this.productService.DiscountProductsQuantityDescendingForPagingAsync(page, itemsPerPage);
                    break;
                default:
                    viewModel.Products = await this.productService.DiscountProductsForPagingAsync(page, itemsPerPage);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = await this.imageService.GetProductTumbnailAsync(product.Id);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search(string search, string order, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            const int itemsPerPage = 3;
            var viewModel = new ProductListViewModel
            {
                Search = search,
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = await this.productService.GetCountBySearchAsync(search),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = await this.productService.SearchProductsPriceAscendingForPagingAsync(page, itemsPerPage, search);
                    break;
                case "PriceDescending":
                    viewModel.Products = await this.productService.SearchProductsPriceDescendingForPagingAsync(page, itemsPerPage, search);
                    break;
                case "QuantityAscending":
                    viewModel.Products = await this.productService.SearchProductsQuantityAscendingForPagingAsync(page, itemsPerPage, search);
                    break;
                case "QuantityDescending":
                    viewModel.Products = await this.productService.SearchProductsQuantityDescendingForPagingAsync(page, itemsPerPage, search);
                    break;
                default:
                    viewModel.Products = await this.productService.SearchProductsForPagingAsync(page, itemsPerPage, search);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = await this.imageService.GetProductTumbnailAsync(product.Id);
            }

            return this.View(viewModel);
        }
    }
}
