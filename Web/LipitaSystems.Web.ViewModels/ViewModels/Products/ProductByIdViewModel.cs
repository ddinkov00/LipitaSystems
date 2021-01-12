namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? PriceAfterDiscount { get; set; }

        public string MainCategoryName { get; set; }

        public string SecondaryCategoryName { get; set; }

        public int QuantityInStock { get; set; }

        public IEnumerable<string> ImagesUlr { get; set; }
    }
}
