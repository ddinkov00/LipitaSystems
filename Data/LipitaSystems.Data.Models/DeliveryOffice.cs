namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class DeliveryOffice : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
