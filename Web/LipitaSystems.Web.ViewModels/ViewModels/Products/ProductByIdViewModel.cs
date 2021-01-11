using System;
using System.Collections.Generic;
using System.Text;

namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    public class ProductByIdViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? PriceAfterDiscount { get; set; }

        public string MainCategoryName { get; set; }

        public string SecondaryCategoryName { get; set; }

        public IEnumerable<string> ImagesUlr { get; set; }
    }
}
