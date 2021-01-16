namespace LipitaSystems.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository;
        private readonly IImageService imageService;

        public CategoryService(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository,
            IImageService imageService)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.secondaryCategoryRepository = secondaryCategoryRepository;
            this.imageService = imageService;
        }

        public async Task AddMainCategory(AddMainCategoryInputModel inputModel)
        {
            var category = new MainCategory
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.Url,
            };

            await this.mainCategoryRepository.AddAsync(category);
            await this.mainCategoryRepository.SaveChangesAsync();
        }

        public async Task<int> AddSecondaryCategory(AddSecondaryCategoryInputModel inputModel)
        {
            var category = new SecondaryCategory
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.ImageUrl,
                MainCategoryId = inputModel.MainCategoryId,
            };

            await this.secondaryCategoryRepository.AddAsync(category);
            await this.secondaryCategoryRepository.SaveChangesAsync();

            return inputModel.MainCategoryId;
        }

        public IEnumerable<MainCategoriesSelectListViewModel> GetAllForSelectList()
        {
            return this.mainCategoryRepository.AllAsNoTracking()
                .Select(mc => new MainCategoriesSelectListViewModel
                {
                    Id = mc.Id,
                    Name = mc.Name,
                    Url = mc.ImageUrl,
                    SecondaryCategories = mc.SecondaryCategories
                        .Select(sc => new SecondaryCategorySelectListViewModel
                        {
                            Id = sc.Id,
                            Name = sc.Name,
                        }),
                })
                .ToList();
        }

        public SubCategoriesViewModel GetAllSubCategoriesForSelectList(int id)
        {
            var model = new SubCategoriesViewModel();
            model.SubCategories = this.secondaryCategoryRepository.AllAsNoTracking()
                .Where(sc => sc.MainCategoryId == id)
                .Select(sc => new SecondaryCategorySelectListViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Count = sc.Products.Count(),
                    Url = sc.ImageUrl,
                })
                .ToList();
            model.Category = this.GetCategoryNameById(id);
            return model;
        }

        public string GetCategoryNameById(int id)
        {
            var category = this.mainCategoryRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            return category.Name;
        }

        public ICollection<MainCategoryForSelectListViewModel> GetMainCategoriesForSelectList()
        {
            return this.mainCategoryRepository.AllAsNoTracking()
                .Select(mc => new MainCategoryForSelectListViewModel
                {
                    Id = mc.Id,
                    Name = mc.Name,
                }).ToList();
        }

        public SecondaryCategory GetSubCategoryNameById(int id)
        {
            return this.secondaryCategoryRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
