namespace LipitaSystems.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class ProductOrder : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Range(0, 10000)]
        public int Quantity { get; set; }
    }
}
