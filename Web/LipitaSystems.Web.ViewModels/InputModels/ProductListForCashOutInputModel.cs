namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public class ProductListForCashOutInputModel
    {
        public ProductListForCashOutInputModel()
        {
            this.Products = new List<ProductForCheckoutViewModel>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string DeliveryType { get; set; }

        [Required]
        public string OptionsForTheDelivery { get; set; }

        public decimal TotalPrice => this.Products.Sum(p => p.FinalPrice);

        public DiscountCodeWithCategoryIds DiscountCode { get; set; }

        public ICollection<ProductForCheckoutViewModel> Products { get; set; }
    }
}
