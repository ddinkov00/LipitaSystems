namespace LipitaSystems.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public interface ICategoryService
    {
        IEnumerable<MainCategoriesSelectListViewModel> GetAllForSelectList();
    }
}
