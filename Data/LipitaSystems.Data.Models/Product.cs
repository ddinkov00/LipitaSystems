namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<ProductOrder>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public int? DiscountPercentage { get; set; }

        public decimal? PriceAfterDiscount => this.DiscountPercentage is not null
            ? this.OriginalPrice - ((this.DiscountPercentage / 100) * this.OriginalPrice)
            : this.OriginalPrice;

        public int QuantityInStock { get; set; }

        public int CategoryId { get; set; }

        public virtual SecondaryCategory Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
