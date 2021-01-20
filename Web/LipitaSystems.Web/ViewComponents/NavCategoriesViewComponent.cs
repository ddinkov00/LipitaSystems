namespace LipitaSystems.Web.ViewComponents
{
    using System;
    using System.Threading.Tasks;

    using LipitaSystems.Common;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class NavCategoriesViewComponent : ViewComponent
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICategoryService categoryService;

        public NavCategoriesViewComponent(
            IMemoryCache memoryCache,
            ICategoryService categoryService)
        {
            this.memoryCache = memoryCache;
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutViewModel viewModel;

            if (!this.memoryCache.TryGetValue(GlobalConstants.LayoutCategoriesKey, out viewModel))
            {
                viewModel = await this.categoryService.GetCategorieseForLayout();

                this.memoryCache.Set(
                    GlobalConstants.LayoutCategoriesKey,
                    viewModel,
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));
            }

            return this.View(viewModel);
        }
    }
}
