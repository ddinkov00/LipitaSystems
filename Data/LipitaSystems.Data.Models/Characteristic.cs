namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class Characteristic : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int HostingSubscriptionId { get; set; }

        public virtual HostingSubscription HostingSubscription { get; set; }
    }
}
