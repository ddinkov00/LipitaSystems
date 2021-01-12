namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class HostingSubscription : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public int? DiscountPercentage { get; set; }
    }
}
