namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class SpecificationInputModel
    {
        [Required(ErrorMessage = "е задължително")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "е задължително")]
        [Display(Name = "Стойност")]
        public string Value { get; set; }
    }
}
