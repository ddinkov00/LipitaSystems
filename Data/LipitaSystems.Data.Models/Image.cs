namespace LipitaSystems.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        [Url]
        public string Url { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
