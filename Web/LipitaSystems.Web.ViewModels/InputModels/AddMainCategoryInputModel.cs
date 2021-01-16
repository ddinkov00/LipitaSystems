namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddMainCategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}
