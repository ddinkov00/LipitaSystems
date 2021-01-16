﻿namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class MainCategory : BaseDeletableModel<int>
    {
        public MainCategory()
        {
            this.SecondaryCategories = new HashSet<SecondaryCategory>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public virtual ICollection<SecondaryCategory> SecondaryCategories { get; set; }
    }
}
