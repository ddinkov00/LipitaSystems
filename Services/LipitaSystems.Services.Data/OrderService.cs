namespace LipitaSystems.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public OrderService(IDeletableEntityRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
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
                TotalPrice = input.TotalPrice,
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
