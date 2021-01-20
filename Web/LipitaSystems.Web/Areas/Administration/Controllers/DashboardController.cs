namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LipitaSystems.Services.Data;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IContactMessageService contactMessageService;

        public DashboardController(
            ISettingsService settingsService,
            IOrderService orderService,
            IProductService productService,
            IContactMessageService contactMessageService)
        {
            this.settingsService = settingsService;
            this.orderService = orderService;
            this.productService = productService;
            this.contactMessageService = contactMessageService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            viewModel.AllOrdersCount = await this.orderService.GetAllOrdersCount();
            viewModel.FinishedOrdersCount = await this.orderService.GetFinishedOrdersCount();
            viewModel.ProductsInStockCount = await this.productService.GetProductsInStockCount();
            viewModel.ContactMessagesCount = await this.contactMessageService.GetNotDeletedCunt();

            return this.View(viewModel);
        }
    }
}
