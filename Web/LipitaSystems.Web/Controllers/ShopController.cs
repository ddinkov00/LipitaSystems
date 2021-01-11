namespace LipitaSystems.Web.Controllers
{
    using System;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly IProductService productService;

        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult AllSubCategories()
        {
            return this.View();
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
    }
}
