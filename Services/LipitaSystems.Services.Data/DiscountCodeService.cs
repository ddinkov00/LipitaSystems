namespace LipitaSystems.Services.Data
{
    using System.Linq;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;

    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDeletableEntityRepository<DiscountCode> discountCodeRepository;

        public DiscountCodeService(IDeletableEntityRepository<DiscountCode> discountCodeRepository)
        {
            this.discountCodeRepository = discountCodeRepository;
        }

        public decimal ApplyDiscount(decimal summedPrice, int categoryId, bool isDiscounted, string code)
        {
            if (code == null)
            {
                return summedPrice;
            }

            var discountCode = this.discountCodeRepository.AllAsNoTracking()
                .Where(dc => dc.Code == code)
                .Select(dc => new DiscountCodeWithCategoryIds
                {
                    DiscountPercentage = dc.DiscountPercentage,
                    DoesWorkOnDiscountedProducts = dc.DoesWorkOnDiscountedProducts,
                    CategoryIds = dc.SecondaryCategories
                        .Select(sc => sc.Id)
                        .ToList(),
                })
                .FirstOrDefault();

            if (isDiscounted && discountCode.DoesWorkOnDiscountedProducts == false)
            {
                return summedPrice;
            }

            if (!discountCode.CategoryIds.Contains(categoryId))
            {
                return summedPrice;
            }

            return summedPrice - ((discountCode.DiscountPercentage / 100) * summedPrice);
        }
    }
}
