namespace LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes
{
    using System.Collections.Generic;

    public class DiscountCodeWithCategoryIds
    {
        public int Id { get; set; }

        public int DiscountPercentage { get; set; }

        public string DiscountName { get; set; }

        public bool DoesWorkOnDiscountedProducts { get; set; }

        public int MainCategoryId { get; set; }
    }
}
