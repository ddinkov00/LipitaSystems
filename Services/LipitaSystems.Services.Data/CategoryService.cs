namespace LipitaSystems.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public CategoryService(IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public IEnumerable<MainCategoriesSelectListViewModel> GetAllForSelectList()
        {
            return this.mainCategoryRepository.AllAsNoTracking()
                .Select(mc => new MainCategoriesSelectListViewModel
                {
                    Name = mc.Name,
                    SecondaryCategories = mc.SecondaryCategories
                        .Select(sc => new SecondaryCategorySelectListViewModel
                        {
                            Id = sc.Id,
                            Name = sc.Name,
                        }),
                });
        }
    }
}
