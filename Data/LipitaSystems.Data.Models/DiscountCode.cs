namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class DiscountCode : BaseDeletableModel<int>
    {
        public DiscountCode()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Code { get; set; }

        [Required]
        [Range(1, 100)]
        public int DiscountPercentage { get; set; }

        [Required]
        public bool DoesWorkOnDiscountedProducts { get; set; }

        public int MainCategoryId { get; set; }

        public virtual MainCategory MainCategory { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
