namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DiscountCodeInputModel
    {
        [Required(ErrorMessage = "задължителен")]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Range(1, 100, ErrorMessage = "трябва да е межу 1 и 100")]
        [Display(Name = "Процент на намалението")]
        public int DiscountPercentage { get; set; }

        [Display(Name = "Ще работи ли върху вече намалени продукти?")]
        public bool DoesWorkOnDiscountedProducts { get; set; }

        [Display(Name = "Върху коя категория ще работи?")]
        public int MainCategoryId { get; set; }
    }
}
