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
        private readonly IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository;

        public CategoryService(IDeletableEntityRepository<MainCategory> mainCategoryRepository,
                               IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.secondaryCategoryRepository = secondaryCategoryRepository;
        }

        public IEnumerable<MainCategoriesSelectListViewModel> GetAllForSelectList()
        {
            return this.mainCategoryRepository.AllAsNoTracking()
                .Select(mc => new MainCategoriesSelectListViewModel
                {
                    Id = mc.Id,
                    Name = mc.Name,
                    SecondaryCategories = mc.SecondaryCategories
                        .Select(sc => new SecondaryCategorySelectListViewModel
                        {
                            Id = sc.Id,
                            Name = sc.Name,
                        }),
                })
                .ToList();
        }

        public IEnumerable<SecondaryCategorySelectListViewModel> GetAllSubCategoriesForSelectList(int id)
        {
            return this.secondaryCategoryRepository.AllAsNoTracking()
                .Where(sc => sc.MainCategoryId == id)
                .Select(sc => new SecondaryCategorySelectListViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Count = sc.Products.Count(),
                })
                .ToList();
        }
    }
}
