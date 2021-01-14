namespace LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes
{
    using System.Collections.Generic;

    public class DiscountCodeWithCategoryIds
    {
        public int DiscountPercentage { get; set; }

        public bool DoesWorkOnDiscountedProducts { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
