namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LipitaSystems.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<ProductOrder>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal OriginalPrice { get; set; }

        public int? DiscountPercentage { get; set; }

        [Range(0, 5000)]
        public int QuantityInStock { get; set; }

        public int CategoryId { get; set; }

        public virtual SecondaryCategory Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
