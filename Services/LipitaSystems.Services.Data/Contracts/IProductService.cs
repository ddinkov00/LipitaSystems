namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;

    public interface IProductService
    {
        Task<int> CreateAsync(ProductInputModel inputModel);
    }
}
