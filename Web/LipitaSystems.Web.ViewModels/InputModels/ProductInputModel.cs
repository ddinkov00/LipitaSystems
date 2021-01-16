﻿namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Web.ViewModels.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required(ErrorMessage = "е задължително")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "е задължително")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [Range(0, 1000, ErrorMessage = "не трябва да е м-у 0 и 1000")]
        public decimal Price { get; set; }

        [Display(Name = "Количество на склад")]
        [Range(0, 5000, ErrorMessage = "не трябва да е по м-у 0 и 5000")]
        public int QuantityInstock { get; set; }

        [Display(Name = "Категория")]
        public int SecondaryCategoryId { get; set; }

        [Required]
        [Display(Name = "Снимки")]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<MainCategoriesSelectListViewModel> CategoryItems { get; set; }
    }
}
