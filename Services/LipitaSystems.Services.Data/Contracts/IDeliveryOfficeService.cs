namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;

    using LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses;

    public interface IDeliveryOfficeService
    {
        ICollection<DeliveryOfficeSelectListViewModel> GetAllForSelectList();
    }
}
