namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public interface IProductService
    {
        Task<int> CreateAsync(ProductInputModel inputModel);

        IEnumerable<ProductInListViewModel> GetAllByCategoryForPaging(int secondaryCategoryId, int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> GetAllByCategoryPriceDescendingForPaging(int secondaryCategoryId, int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> GetAllByCategoryPriceAscendingForPaging(int secondaryCategoryId, int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> GetAllByCategoryQuantityAscendingForPaging(int secondaryCategoryId, int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> GetAllByCategoryQuantityDescendingForPaging(int secondaryCategoryId, int page, int itemsPerPage);

        ProductByIdViewModel GetById(int productId);

        int GetAllCountByCategory(int categoryid);

        int GetCountBySearch(string search);

        int GetCountByDiscount();

        IEnumerable<ProductInListViewModel> DiscountProductsForPaging(int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> DiscountProductsPriceAscendingForPaging(int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> DiscountProductsPriceDescendingForPaging(int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> DiscountProductsQuantityAscendingForPaging(int page, int itemsPerPage);

        IEnumerable<ProductInListViewModel> DiscountProductsQuantityDescendingForPaging(int page, int itemsPerPage);


        IEnumerable<CartViewModel> GetProductsForCart(Dictionary<int, int> products);

        IEnumerable<ProductInListViewModel> SearchProductsForPaging(int page, int itemsPerPage, string search);

        IEnumerable<ProductInListViewModel> SearchProductsPriceDescendingForPaging(int page, int itemsPerPage, string search);

        IEnumerable<ProductInListViewModel> SearchProductsPriceAscendingForPaging(int page, int itemsPerPage, string search);

        IEnumerable<ProductInListViewModel> SearchProductsQuantityDescendingForPaging(int page, int itemsPerPage, string search);

        IEnumerable<ProductInListViewModel> SearchProductsQuantityAscendingForPaging(int page, int itemsPerPage, string search);

    }
}
