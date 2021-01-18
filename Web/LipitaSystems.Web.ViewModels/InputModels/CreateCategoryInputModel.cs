namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel
    {
        [Required(ErrorMessage = "* задължително")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* задължително")]
        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }
    }
}
