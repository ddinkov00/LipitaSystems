namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses;

    public interface IDeliveryOfficeService
    {
        Task<ICollection<DeliveryOfficeSelectListViewModel>> GetAllForSelectListAsync();
    }
}
