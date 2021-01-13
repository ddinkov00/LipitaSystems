namespace LipitaSystems.Services.Data.Contracts
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Models;
    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public interface ICategoryService
    {
        IEnumerable<MainCategoriesSelectListViewModel> GetAllForSelectList();

        SubCategoriesViewModel GetAllSubCategoriesForSelectList(int id);

        string GetCategoryNameById(int id);

        SecondaryCategory GetSubCategoryNameById(int id);
    }
}
