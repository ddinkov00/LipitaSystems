using System;
using System.Collections.Generic;

namespace LipitaSystems.Web.ViewModels.ViewModels.Categories
{
    public class SubCategoriesViewModel
    {
        public SubCategoriesViewModel()
        {
        }

        public string Category { get; set; }

        public List<SecondaryCategorySelectListViewModel> subCategories { get; set; }
    }
}
