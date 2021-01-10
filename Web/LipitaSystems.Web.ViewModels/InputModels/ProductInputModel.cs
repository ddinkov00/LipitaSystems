namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Web.ViewModels.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество на склад")]
        public int QuantityInstock { get; set; }

        [Display(Name = "Категория")]
        public int SecondaryCategoryId { get; set; }

        [Required]
        [Display(Name = "Снимки")]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<MainCategoriesSelectListViewModel> CategoryItems { get; set; }
    }
}
