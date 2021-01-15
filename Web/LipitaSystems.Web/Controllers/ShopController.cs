namespace LipitaSystems.Web.Controllers
{
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

        public IActionResult Products(int id, string order, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            const int itemsPerPage = 3;
            var subCategory = this.categoryService.GetSubCategoryNameById(id);
            var category = this.categoryService.GetCategoryNameById(subCategory.MainCategoryId);
            var viewModel = new ProductListViewModel
            {
                Category = category,
                Id = subCategory.MainCategoryId,
                SubCategory = subCategory.Name,
                SubCategoryId = id,
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                ItemsCount = this.productService.GetAllCountByCategory(id),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = this.productService.GetAllByCategoryPriceAscendingForPaging(id, page, itemsPerPage);
                    break;
                case "PriceDescending":
                    viewModel.Products = this.productService.GetAllByCategoryPriceDescendingForPaging(id, page, itemsPerPage);
                    break;
                case "QuantityAscending":
                    viewModel.Products = this.productService.GetAllByCategoryQuantityAscendingForPaging(id, page, itemsPerPage);
                    break;
                case "QuantityDescending":
                    viewModel.Products = this.productService.GetAllByCategoryQuantityDescendingForPaging(id, page, itemsPerPage);
                    break;
                default:
                    viewModel.Products = this.productService.GetAllByCategoryForPaging(id, page, itemsPerPage);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = this.imageService.GetProductTumbnail(product.Id);
            }

            return this.View(viewModel);
        }

        public IActionResult Product(int id)
        {
            var viewModel = this.productService.GetById(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
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

                    viewModel = this.productService.GetProductsForCart(products).ToList();
                }
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Cart(string discountCode)
        {
            return this.RedirectToAction(nameof(this.Checkout), new { discountCode = discountCode });
        }

        public IActionResult Checkout(string discountCode)
        {
            var cookies = this.HttpContext.Request.Cookies["cartProducts"].Split('_');
            var viewModel = new ProductListForCashOutInputModel();

            if (cookies.Any())
            {
                var code = this.discountCodeService.GetDiscountCode(discountCode);
                viewModel.DiscountCode = code;

                for (int i = 0; i < cookies.Length; i += 2)
                {
                    var productId = int.Parse(cookies[i]);
                    var productQuantity = int.Parse(cookies[i + 1]);

                    viewModel.Products.Add(this.productService
                        .GetProductForCheckoutById(productId, productQuantity, code));
                }
            }

            viewModel.DeliveryOfficeItems = this.deliveryOfficeService.GetAllForSelectList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ProductListForCashOutInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                // selectList items ad products !!!!!
                return this.View(input);
            }

            var cookies = this.HttpContext.Request.Cookies["cartProducts"].Split('_');

            if (cookies.Any())
            {
                for (int i = 0; i < cookies.Length; i += 2)
                {
                    var productId = int.Parse(cookies[i]);
                    var productQuantity = int.Parse(cookies[i + 1]);

                    await this.productService.ReduceQuantityInStock(productId, productQuantity);

                    input.Products.Add(this.productService
                        .GetProductForCheckoutById(productId, productQuantity, null));
                }
            }

            await this.orderService.MakeOrder(input);
            return this.Redirect("/");
        }

        public IActionResult Discount(string order, int page = 1)
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
                ItemsCount = this.productService.GetCountByDiscount(),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = this.productService.DiscountProductsPriceAscendingForPaging(page, itemsPerPage);
                    break;
                case "PriceDescending":
                    viewModel.Products = this.productService.DiscountProductsPriceDescendingForPaging(page, itemsPerPage);
                    break;
                case "QuantityAscending":
                    viewModel.Products = this.productService.DiscountProductsQuantityAscendingForPaging(page, itemsPerPage);
                    break;
                case "QuantityDescending":
                    viewModel.Products = this.productService.DiscountProductsQuantityDescendingForPaging(page, itemsPerPage);
                    break;
                default:
                    viewModel.Products = this.productService.DiscountProductsForPaging(page, itemsPerPage);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = this.imageService.GetProductTumbnail(product.Id);
            }

            return this.View(viewModel);
        }

        public IActionResult Search(string search, string order, int page = 1)
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
                ItemsCount = this.productService.GetCountBySearch(search),
            };

            switch (order)
            {
                case "PriceAscending":
                    viewModel.Products = this.productService.SearchProductsPriceAscendingForPaging(page, itemsPerPage, search);
                    break;
                case "PriceDescending":
                    viewModel.Products = this.productService.SearchProductsPriceDescendingForPaging(page, itemsPerPage, search);
                    break;
                case "QuantityAscending":
                    viewModel.Products = this.productService.SearchProductsQuantityAscendingForPaging(page, itemsPerPage, search);
                    break;
                case "QuantityDescending":
                    viewModel.Products = this.productService.SearchProductsQuantityDescendingForPaging(page, itemsPerPage, search);
                    break;
                default:
                    viewModel.Products = this.productService.SearchProductsForPaging(page, itemsPerPage, search);
                    break;
            }

            foreach (var product in viewModel.Products)
            {
                product.ImageUrl = this.imageService.GetProductTumbnail(product.Id);
            }

            return this.View(viewModel);
        }
    }
}
