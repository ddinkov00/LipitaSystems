namespace LipitaSystems.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public OrderService(IDeletableEntityRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<int> GetAllOrdersCount()
        {
            return await this.orderRepository.AllAsNoTracking()
                .CountAsync();
        }

        public async Task<int> GetFinishedOrdersCount()
        {
            return await this.orderRepository.AllAsNoTracking()
                .Where(o => o.IsDeleted)
                .CountAsync();
        }

        public async Task MakeOrder(ProductListForCashOutInputModel input)
        {
            await this.orderRepository.AddAsync(new Order
            {
                Address = input.Address,
                DeliveryNotes = input.OptionsForTheDelivery,
                FullName = input.FullName,
                DeliveryType = input.DeliveryType,
                PhoneNumber = input.PhoneNumber,
                DiscountCodeId = input.DiscountCode == null ? null : input.DiscountCode.Id,
                TotalPrice = (decimal)input.TotalPrice,
                Products = input.Products.Select(x => new ProductOrder
                {
                    ProductId = x.Id,
                    Quantity = x.Quantity,
                }).ToList(),
                DeliveryOfficeId = input.DeliveryOfficeId != null ? (int)input.DeliveryOfficeId : null,
            });

            await this.orderRepository.SaveChangesAsync();
        }
    }
}
