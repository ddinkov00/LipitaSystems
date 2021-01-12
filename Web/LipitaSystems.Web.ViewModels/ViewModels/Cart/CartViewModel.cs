namespace LipitaSystems.Web.ViewModels.ViewModels.Cart
{
    using System;

    public class CartViewModel
    {
        public CartViewModel()
        {
        }

        public int Id { get; set; }

        public string Product { get; set; }

        public string SubCategory { get; set; }

        public int SubCategoryId { get; set; }

        public int Quantity { get; set; }

        public int ProductMaxQuantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
