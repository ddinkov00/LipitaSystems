namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public class AddSecondaryCategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        public int MainCategoryId { get; set; }

        public ICollection<MainCategoryForSelectListViewModel> MainCategoryItems { get; set; }
    }
}
