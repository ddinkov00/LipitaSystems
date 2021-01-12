namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    using System;
    using System.Collections.Generic;

    public class ProductByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public int? DiscountPercentage { get; set; }

        public decimal? PriceAfterDiscout => this.DiscountPercentage == null
            ? null
            : decimal.Round(
                this.OriginalPrice - (((decimal)this.DiscountPercentage / 100) * this.OriginalPrice),
                2,
                MidpointRounding.AwayFromZero);

        public string MainCategoryName { get; set; }

        public string SecondaryCategoryName { get; set; }

        public int QuantityInStock { get; set; }

        public IEnumerable<string> ImagesUlr { get; set; }
    }
}
