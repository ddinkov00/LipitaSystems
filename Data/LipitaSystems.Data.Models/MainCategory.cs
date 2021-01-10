namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class MainCategory : BaseDeletableModel<int>
    {
        public MainCategory()
        {
            this.SecondaryCategories = new HashSet<SecondaryCategory>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<SecondaryCategory> SecondaryCategories { get; set; }
    }
}
