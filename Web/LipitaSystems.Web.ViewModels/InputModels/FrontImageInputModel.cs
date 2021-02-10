using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LipitaSystems.Web.ViewModels.InputModels
{
    public class FrontImageInputModel
    {
        [Required(ErrorMessage = "* задължително")]
        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "* задължително")]
        [Display(Name = "Заглавие")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "* задължително")]
        [Display(Name = "Съдърждание")]
        public string Content { get; set; }

        [Display(Name = "Къде да праща (URL)")]
        public string RedirectUrl { get; set; }

        [Display(Name = "Място на подредба")]
        public int? Order { get; set; }

    }
}
