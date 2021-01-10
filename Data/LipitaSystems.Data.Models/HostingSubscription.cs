namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class HostingSubscription : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public int? DiscountPercentage { get; set; }

        public decimal? PriceAfterDiscount => this.DiscountPercentage is not null
            ? this.OriginalPrice - ((this.DiscountPercentage / 100) * this.OriginalPrice)
            : this.OriginalPrice;
    }
}
