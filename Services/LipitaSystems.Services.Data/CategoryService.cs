namespace LipitaSystems.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using LipitaSystems.Web.ViewModels.ViewModels;
    using LipitaSystems.Web.ViewModels.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;

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

        public async Task AddMainCategoryAsync(AddMainCategoryInputModel inputModel)
        {
            var category = new MainCategory
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.Url,
            };

            await this.mainCategoryRepository.AddAsync(category);
            await this.mainCategoryRepository.SaveChangesAsync();
        }

        public async Task<int> AddSecondaryCategoryAsync(AddSecondaryCategoryInputModel inputModel)
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

        public async Task<IEnumerable<MainCategoriesSelectListViewModel>> GetAllForSelectListAsync()
        {
            return await this.mainCategoryRepository.AllAsNoTracking()
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
                }).ToListAsync();
        }

        public async Task<SubCategoriesViewModel> GetAllSubCategoriesForSelectListAsync(int id)
        {
            var model = new SubCategoriesViewModel();
            model.SubCategories = await this.secondaryCategoryRepository.AllAsNoTracking()
                .Where(sc => sc.MainCategoryId == id)
                .Select(sc => new SecondaryCategorySelectListViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Count = sc.Products.Count(),
                    Url = sc.ImageUrl,
                })
                .ToListAsync();

            model.Category = await this.GetCategoryNameByIdAsync(id);
            return model;
        }

        public async Task<LayoutViewModel> GetCategorieseForLayout()
        {
            var categoriesViewModel = new LayoutViewModel
            {
                MainCategories = await this.mainCategoryRepository.All()
                    .Select(mc => new MainCategoryForLayoutViewModel
                    {
                        Id = mc.Id,
                        Name = mc.Name,
                        SecondaryCategories = mc.SecondaryCategories
                            .Select(sc => new SecondaryCategoryForLayoutViewModel
                            {
                                Id = sc.Id,
                                Name = sc.Name,
                            }).ToList(),
                    }).ToListAsync(),
            };

            return categoriesViewModel;
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            var category = await this.mainCategoryRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return category.Name;
        }

        public async Task<ICollection<MainCategoryForSelectListViewModel>> GetMainCategoriesForSelectListAsync()
        {
            return await this.mainCategoryRepository.AllAsNoTracking()
                .Select(mc => new MainCategoryForSelectListViewModel
                {
                    Id = mc.Id,
                    Name = mc.Name,
                }).ToListAsync();
        }

        public async Task<SecondaryCategory> GetSubCategoryNameByIdAsync(int id)
        {
            return await this.secondaryCategoryRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
