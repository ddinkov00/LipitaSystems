namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class DiscountCode : BaseDeletableModel<int>
    {
        public DiscountCode()
        {
            this.SecondaryCategories = new HashSet<SecondaryCategory>();
        }

        public string Code { get; set; }

        public int DiscountPercentage { get; set; }

        public bool DoesWorkOnDiscountedProducts { get; set; }

        public virtual ICollection<SecondaryCategory> SecondaryCategories { get; set; }
    }
}
