namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;

    public interface IDiscountCodeService
    {
        decimal ApplyDiscount(DiscountCodeWithCategoryIds code, bool isDiscounted, int categoryId, decimal summedPrice);

        Task<DiscountCodeWithCategoryIds> GetDiscountCodeAsync(string code);

        Task<bool> IsDiscountCodeValid(string code);
    }
}
