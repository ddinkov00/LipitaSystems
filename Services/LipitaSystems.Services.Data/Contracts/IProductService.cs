namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Cart;
    using LipitaSystems.Web.ViewModels.ViewModels.Discount_Codes;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public interface IProductService
    {
        Task<int> GetProductsInStockCount();

        Task<int> CreateAsync(ProductInputModel inputModel);

        Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryPriceDescendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryPriceAscendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryQuantityAscendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> GetAllByCategoryQuantityDescendingForPagingAsync(int secondaryCategoryId, int page, int itemsPerPage);

        Task<ProductByIdViewModel> GetByIdAsync(int productId);

        Task<ProductForCheckoutViewModel> GetProductForCheckoutByIdAsync(int id, int quantity, DiscountCodeWithCategoryIds code);

        Task<int> GetAllCountByCategoryAsync(int categoryid);

        Task<int> GetCountBySearchAsync(string search);

        Task<int> GetCountByDiscountAsync();

        Task ReduceQuantityInStockAsync(int id, int boughtQuantity);

        Task<IEnumerable<ProductInListViewModel>> DiscountProductsForPagingAsync(int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> DiscountProductsPriceAscendingForPagingAsync(int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> DiscountProductsPriceDescendingForPagingAsync(int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> DiscountProductsQuantityAscendingForPagingAsync(int page, int itemsPerPage);

        Task<IEnumerable<ProductInListViewModel>> DiscountProductsQuantityDescendingForPagingAsync(int page, int itemsPerPage);

        Task<List<CartViewModel>> GetProductsForCart(Dictionary<int, int> products);

        Task<IEnumerable<ProductInListViewModel>> SearchProductsForPagingAsync(int page, int itemsPerPage, string search);

        Task<IEnumerable<ProductInListViewModel>> SearchProductsPriceDescendingForPagingAsync(int page, int itemsPerPage, string search);

        Task<IEnumerable<ProductInListViewModel>> SearchProductsPriceAscendingForPagingAsync(int page, int itemsPerPage, string search);

        Task<IEnumerable<ProductInListViewModel>> SearchProductsQuantityDescendingForPagingAsync(int page, int itemsPerPage, string search);

        Task<IEnumerable<ProductInListViewModel>> SearchProductsQuantityAscendingForPagingAsync(int page, int itemsPerPage, string search);
    }
}
