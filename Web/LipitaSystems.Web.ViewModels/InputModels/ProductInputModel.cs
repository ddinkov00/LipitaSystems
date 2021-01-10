using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LipitaSystems.Web.ViewModels.InputModels
{
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
    }
}
