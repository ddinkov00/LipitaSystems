namespace LipitaSystems.Web.ViewModels.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public class ProductListForCashOutInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public decimal TotalPrice => this.Products.Sum(p => p.FinalPrice);

        public ICollection<ProductForCheckoutViewModel> Products { get; set; }
    }
}
