namespace LipitaSystems.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses;
    using Microsoft.EntityFrameworkCore;

    public class DeliveryOfficeService : IDeliveryOfficeService
    {
        private readonly IDeletableEntityRepository<DeliveryOffice> deliveryOfficeRepository;

        public DeliveryOfficeService(IDeletableEntityRepository<DeliveryOffice> deliveryOfficeRepository)
        {
            this.deliveryOfficeRepository = deliveryOfficeRepository;
        }

        public async Task<ICollection<DeliveryOfficeSelectListViewModel>> GetAllForSelectListAsync()
        {
            return await this.deliveryOfficeRepository.AllAsNoTracking()
                .Select(o => new DeliveryOfficeSelectListViewModel
                {
                    Id = o.Id.ToString(),
                    Name = o.Name,
                    Address = o.Address,
                }).ToListAsync();
        }
    }
}
