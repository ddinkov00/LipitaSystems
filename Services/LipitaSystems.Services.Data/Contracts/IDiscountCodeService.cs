namespace LipitaSystems.Services.Data.Contracts
{

    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;

    public interface IDiscountCodeService
    {
        decimal ApplyDiscount(DiscountCodeWithCategoryIds code, bool isDiscounted, int categoryId, decimal summedPrice);

        DiscountCodeWithCategoryIds GetDiscountCode(string code);
    }
}
