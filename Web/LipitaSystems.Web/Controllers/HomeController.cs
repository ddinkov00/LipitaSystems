namespace LipitaSystems.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IContactMessageService contactMessageService;
        private readonly INewsService newsService;

        public HomeController(IContactMessageService contactMessageService, INewsService newsService)
        {
            this.contactMessageService = contactMessageService;
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            return this.View();
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
