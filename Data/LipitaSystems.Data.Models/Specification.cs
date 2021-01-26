namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class Specification : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
