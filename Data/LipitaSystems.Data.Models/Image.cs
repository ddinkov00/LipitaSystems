namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Url { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
