namespace LipitaSystems.Data.Models
{
    using LipitaSystems.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
