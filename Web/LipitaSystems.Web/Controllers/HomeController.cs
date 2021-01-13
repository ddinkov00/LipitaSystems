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

        public HomeController(IContactMessageService contactMessageService)
        {
            this.contactMessageService = contactMessageService;
        }

        public IActionResult Index()
        {
            return this.View();
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

            await this.contactMessageService.Send(input);
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
