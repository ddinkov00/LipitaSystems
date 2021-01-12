namespace LipitaSystems.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
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

        public IActionResult Products(int id, int secondaryCategoryId)
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

        public IActionResult Cart()
        {
            var viewModel = new List<CartViewModel>();

            if (this.HttpContext.Request.Cookies.ContainsKey("cartProduct"))
            {
                var cookies = this.HttpContext.Request.Cookies["cartProduct"].Split('_');
                for (int i = 0; i < cookies.Length; i += 3)
                {
                    viewModel.Add(new CartViewModel
                    {
                        Product = cookies[i],
                        Quantity = cookies[i + 1],
                        Price = cookies[i + 2],

                    });
                }
            }

            return this.View(viewModel);
        }
    }
}
