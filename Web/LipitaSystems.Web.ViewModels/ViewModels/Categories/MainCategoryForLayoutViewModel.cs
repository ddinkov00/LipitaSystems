namespace LipitaSystems.Web.ViewModels.ViewModels.Categories
{
    using System.Collections.Generic;

    public class MainCategoryForLayoutViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public ICollection<SecondaryCategoryForLayoutViewModel> SecondaryCategories { get; set; }
    }
}
