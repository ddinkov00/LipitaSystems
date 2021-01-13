namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormInputModel
    {
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = " е задължителна!")]
        [Display(Name = "Обратна връзка")]
        public string Contact { get; set; }

        [Required(ErrorMessage = " е задължителна!")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessage = " е задължително!")]
        [MinLength(10)]
        [Display(Name = "Съобщение")]
        public string Message { get; set; }
    }
}
