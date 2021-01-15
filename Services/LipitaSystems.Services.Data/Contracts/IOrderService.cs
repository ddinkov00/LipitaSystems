using System;
using System.Threading.Tasks;
using LipitaSystems.Web.ViewModels.InputModels;

namespace LipitaSystems.Services.Data.Contracts
{
    public interface IOrderService
    {
        Task MakeOrder(ProductListForCashOutInputModel input);
    }
}
