namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using LipitaSystems.Common;
    using LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses;
    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public class ProductListForCashOutInputModel
    {
        public ProductListForCashOutInputModel()
        {
            this.Products = new List<ProductForCheckoutViewModel>();
        }

        [Required(ErrorMessage = "са задължителни")]
        //[RegularExpression(GlobalConstants.AtLeastTwoNamesRegex, ErrorMessage = "Имената трябва да са поне две")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Невалиден телефон")]
        [Required(ErrorMessage = "е задължителен")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "е задължителен")]
        public string Address { get; set; }

        [Required]
        public string DeliveryType { get; set; }

        [MaxLength(500, ErrorMessage = "не трябва да надхвърля 500 символа")]
        public string OptionsForTheDelivery { get; set; }

        public string DiscountCodeName { get; set; }

        public decimal TotalPrice => this.Products.Sum(p => p.FinalPrice);

        public int? DeliveryOfficeId { get; set; }

        public DiscountCodeWithCategoryIds DiscountCode { get; set; }

        public ICollection<DeliveryOfficeSelectListViewModel> DeliveryOfficeItems { get; set; }

        public ICollection<ProductForCheckoutViewModel> Products { get; set; }
    }
}
