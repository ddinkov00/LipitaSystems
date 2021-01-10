namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;

    public interface IProductService
    {
        Task<int> CreateAsync(ProductInputModel inputModel);

        IEnumerable<ProductInListViewModel> GetAllByCategoryForPaging(int secondaryCategoryId, int page, int itemsPerPage);
    }
}
