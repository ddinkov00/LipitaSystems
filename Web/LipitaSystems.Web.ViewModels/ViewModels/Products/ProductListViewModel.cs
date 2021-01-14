namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductListViewModel : PagingViewModel
    {
        public string Search { get; set; }

        public string Category { get; set; }

        public int Id { get; set; }

        public string SubCategory { get; set; }

        public int SubCategoryId { get; set; }

        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
