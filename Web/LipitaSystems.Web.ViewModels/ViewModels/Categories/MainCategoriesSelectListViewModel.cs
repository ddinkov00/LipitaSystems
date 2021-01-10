namespace LipitaSystems.Web.ViewModels.ViewModels.Categories
{
    using System.Collections.Generic;

    public class MainCategoriesSelectListViewModel
    {
        public string Name { get; set; }

        public IEnumerable<SecondaryCategorySelectListViewModel> SecondaryCategories { get; set; }
    }
}
