namespace LipitaSystems.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using LipitaSystems.Data.Common.Models;

    public class FrontImage : BaseDeletableModel<int>
    {
        public FrontImage()
        {
        }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Heading { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string RedirectUrl { get; set; }

        public int? Order { get; set; }
    }
}
