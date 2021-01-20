namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;

    public interface IOrderService
    {
        Task MakeOrder(ProductListForCashOutInputModel input);

        Task<int> GetAllOrdersCount();

        Task<int> GetFinishedOrdersCount();
    }
}
