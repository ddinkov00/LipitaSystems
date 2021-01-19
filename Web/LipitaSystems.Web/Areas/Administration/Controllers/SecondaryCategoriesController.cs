namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class SecondaryCategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository;
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public SecondaryCategoriesController(
            IDeletableEntityRepository<SecondaryCategory> secondaryCategoryRepository,
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary)
        {
            this.secondaryCategoryRepository = secondaryCategoryRepository;
            this.mainCategoryRepository = mainCategoryRepository;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
        }

        // GET: Administration/SecondaryCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.secondaryCategoryRepository.All().Include(s => s.MainCategory);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/SecondaryCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var secondaryCategory = await this.secondaryCategoryRepository.All()
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (secondaryCategory == null)
            {
                return this.NotFound();
            }

            return this.View(secondaryCategory);
        }

        // GET: Administration/SecondaryCategories/Create
        public IActionResult Create()
        {
            this.ViewData["MainCategories"] = new SelectList(
                this.mainCategoryRepository.All()
                    .OrderBy(mc => mc.Name),
                "Id",
                "Name");

            return this.View();
        }

        // POST: Administration/SecondaryCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Image,MainCategoryId")] CreateSecondaryCategoryInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var imageUrl = await this.cloudinaryService.UploadAsyncSingle(this.cloudinary, input.Image);

                var category = new SecondaryCategory
                {
                    Name = input.Name,
                    MainCategoryId = input.MainCategoryId,
                    ImageUrl = imageUrl,
                };

                await this.secondaryCategoryRepository.AddAsync(category);
                await this.secondaryCategoryRepository.SaveChangesAsync();



                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["MainCategories"] = new SelectList(this.mainCategoryRepository.All(), "Id", "Name", input.MainCategoryId);
            return this.View(input);
        }

        // GET: Administration/SecondaryCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var secondaryCategory = await this.secondaryCategoryRepository.All()
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (secondaryCategory == null)
            {
                return this.NotFound();
            }

            this.ViewData["MainCategoryId"] = new SelectList(this.mainCategoryRepository.All(), "Id", "Name", secondaryCategory.MainCategoryId);
            return this.View(secondaryCategory);
        }

        // POST: Administration/SecondaryCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ImageUrl,MainCategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] SecondaryCategory secondaryCategory)
        {
            if (id != secondaryCategory.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.secondaryCategoryRepository.Update(secondaryCategory);
                    await this.secondaryCategoryRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.SecondaryCategoryExists(secondaryCategory.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["MainCategoryId"] = new SelectList(this.mainCategoryRepository.All(), "Id", "Name", secondaryCategory.MainCategoryId);
            return this.View(secondaryCategory);
        }

        // GET: Administration/SecondaryCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var secondaryCategory = await this.secondaryCategoryRepository.All()
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (secondaryCategory == null)
            {
                return this.NotFound();
            }

            return this.View(secondaryCategory);
        }

        // POST: Administration/SecondaryCategories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secondaryCategory = await this.secondaryCategoryRepository.All()
                .FirstOrDefaultAsync(sc => sc.Id == id);
            this.secondaryCategoryRepository.Delete(secondaryCategory);
            await this.secondaryCategoryRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> SecondaryCategoryExists(int id)
        {
            return await this.secondaryCategoryRepository.All()
                .AnyAsync(e => e.Id == id);
        }
    }
}
