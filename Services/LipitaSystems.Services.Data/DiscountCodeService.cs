namespace LipitaSystems.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;
    using Microsoft.EntityFrameworkCore;

    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDeletableEntityRepository<DiscountCode> discountCodeRepository;

        public DiscountCodeService(IDeletableEntityRepository<DiscountCode> discountCodeRepository)
        {
            this.discountCodeRepository = discountCodeRepository;
        }

        public async Task<DiscountCodeWithCategoryIds> GetDiscountCodeAsync(string code)
        {
            return await this.discountCodeRepository.AllAsNoTracking()
               .Where(dc => dc.Code == code)
               .Select(dc => new DiscountCodeWithCategoryIds
               {
                   DiscountPercentage = dc.DiscountPercentage,
                   DoesWorkOnDiscountedProducts = dc.DoesWorkOnDiscountedProducts,
                   DiscountName = dc.Code,
                   CategoryIds = dc.SecondaryCategories
                       .Select(sc => sc.Id)
                       .ToList(),
               })
               .FirstOrDefaultAsync();
        }

        public decimal ApplyDiscount(DiscountCodeWithCategoryIds discountCode, bool isDiscounted, int categoryId, decimal summedPrice)
        {
            if (!discountCode.CategoryIds.Contains(categoryId))
            {
                return summedPrice;
            }

            if (isDiscounted && discountCode.DoesWorkOnDiscountedProducts == false)
            {
                return summedPrice;
            }

            return summedPrice - ((discountCode.DiscountPercentage / 100) * summedPrice);
        }
    }
}
