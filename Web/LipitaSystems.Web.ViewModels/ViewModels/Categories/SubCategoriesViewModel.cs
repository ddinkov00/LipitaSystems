namespace LipitaSystems.Web.ViewModels.ViewModels.Categories
{
    using System.Collections.Generic;

    public class SubCategoriesViewModel
    {
        public SubCategoriesViewModel()
        {
        }

        public string Category { get; set; }

        public List<SecondaryCategorySelectListViewModel> SubCategories { get; set; }
    }
}
