namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using LipitaSystems.Services.Data;
    using LipitaSystems.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
