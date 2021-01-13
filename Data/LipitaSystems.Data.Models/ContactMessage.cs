namespace LipitaSystems.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class ContactMessage : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MinLength(10)]
        public string Message { get; set; }
    }
}
