namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Models;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public interface ICategoryService
    {
        Task<IEnumerable<MainCategoriesSelectListViewModel>> GetAllForSelectListAsync();

        Task<SubCategoriesViewModel> GetAllSubCategoriesForSelectListAsync(int id);

        Task<string> GetCategoryNameByIdAsync(int id);

        Task<SecondaryCategory> GetSubCategoryNameByIdAsync(int id);

        Task AddMainCategoryAsync(AddMainCategoryInputModel inputModel);

        Task<int> AddSecondaryCategoryAsync(AddSecondaryCategoryInputModel inputModel);

        Task<ICollection<MainCategoryForSelectListViewModel>> GetMainCategoriesForSelectListAsync();
    }
}
