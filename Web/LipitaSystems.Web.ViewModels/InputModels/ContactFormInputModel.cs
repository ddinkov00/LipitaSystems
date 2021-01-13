namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormInputModel
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Съобщение")]
        public string Message { get; set; }
    }
}
