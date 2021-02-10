namespace LipitaSystems.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : BaseController
    {
        private readonly IContactMessageService contactMessageService;
        private readonly ICategoryService categoryService;
        private readonly INewsService newsService;
        private readonly IMemoryCache memoryCache;
        private readonly IDeletableEntityRepository<FrontImage> frontImageRepo;

        public HomeController(
            IContactMessageService contactMessageService,
            ICategoryService categoryService,
            INewsService newsService,
            IMemoryCache memoryCache,
            IDeletableEntityRepository<FrontImage> frontImageRepo)
        {
            this.contactMessageService = contactMessageService;
            this.categoryService = categoryService;
            this.newsService = newsService;
            this.memoryCache = memoryCache;
            this.frontImageRepo = frontImageRepo;
        }

        public IActionResult Index()
        {
            var viewModel = this.frontImageRepo.AllAsNoTracking().OrderBy(x => x.Order).Select(x => new FrontImageViewModel
            {
                ImageUrl = x.ImgUrl,
                Heading = x.Heading,
                Content = x.Content,
                RedirectUrl = x.RedirectUrl,
            }).ToList();
            return this.View(viewModel);
        }

        public IActionResult News()
        {
            var model = this.newsService.GetLastNews(10);
            return this.View(model);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.contactMessageService.SendAsync(input);
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
