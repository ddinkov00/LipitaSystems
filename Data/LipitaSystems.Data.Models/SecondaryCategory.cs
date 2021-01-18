namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class SecondaryCategory : BaseDeletableModel<int>
    {
        public SecondaryCategory()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        public string Name { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        public int MainCategoryId { get; set; }

        public virtual MainCategory MainCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
    }
}
