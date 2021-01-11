namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductInListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? PriceAfterDiscout { get; set; }
    }
}
