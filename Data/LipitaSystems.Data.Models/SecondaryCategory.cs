namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class SecondaryCategory : BaseDeletableModel<int>
    {
        public SecondaryCategory()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int MainCategoryId { get; set; }

        public virtual MainCategory MainCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
