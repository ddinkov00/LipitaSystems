using LipitaSystems.Services.Data.Contracts;
using LipitaSystems.Web.Controllers;
using LipitaSystems.Web.ViewModels.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult AddSecondaryCategory()
        {
            var viewModel = new AddSecondaryCategoryInputModel();
            viewModel.MainCategoryItems = this.categoryService.GetMainCategoriesForSelectList();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSecondaryCategory(AddSecondaryCategoryInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var mainCategoryId = await this.categoryService.AddSecondaryCategory(inputModel);

            return this.RedirectToAction(
                nameof(ShopController.AllSubCategories),
                nameof(ShopController).Replace("Controller", string.Empty),
                new { id = mainCategoryId });
        }
    }
}
