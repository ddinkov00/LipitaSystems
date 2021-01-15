namespace LipitaSystems.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses;

    public class DeliveryOfficeService : IDeliveryOfficeService
    {
        private readonly IDeletableEntityRepository<DeliveryOffice> deliveryOfficeRepository;

        public DeliveryOfficeService(IDeletableEntityRepository<DeliveryOffice> deliveryOfficeRepository)
        {
            this.deliveryOfficeRepository = deliveryOfficeRepository;
        }

        public ICollection<DeliveryOfficeSelectListViewModel> GetAllForSelectList()
        {
            return this.deliveryOfficeRepository.AllAsNoTracking()
                .Select(o => new DeliveryOfficeSelectListViewModel
                {
                    Id = o.Id.ToString(),
                    Name = o.Name,
                    Address = o.Address,
                }).ToList();
        }
    }
}
