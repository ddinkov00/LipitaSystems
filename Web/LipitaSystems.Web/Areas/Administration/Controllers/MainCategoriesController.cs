namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class MainCategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public MainCategoriesController(IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        // GET: Administration/MainCategories
        public async Task<IActionResult> Index()
        {
            return this.View(await this.mainCategoryRepository.All().ToListAsync());
        }

        // GET: Administration/MainCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var mainCategory = await this.mainCategoryRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainCategory == null)
            {
                return this.NotFound();
            }

            return this.View(mainCategory);
        }

        // GET: Administration/MainCategories/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/MainCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] MainCategory mainCategory)
        {
            if (this.ModelState.IsValid)
            {
                await this.mainCategoryRepository.AddAsync(mainCategory);
                await this.mainCategoryRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(mainCategory);
        }

        // GET: Administration/MainCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var mainCategory = await this.mainCategoryRepository.All()
                .FirstOrDefaultAsync(mc => mc.Id == id);

            if (mainCategory == null)
            {
                return this.NotFound();
            }

            return this.View(mainCategory);
        }

        // POST: Administration/MainCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] MainCategory mainCategory)
        {
            if (id != mainCategory.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.mainCategoryRepository.Update(mainCategory);
                    await this.mainCategoryRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.MainCategoryExists(mainCategory.Id))
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

            return this.View(mainCategory);
        }

        // GET: Administration/MainCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var mainCategory = await this.mainCategoryRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainCategory == null)
            {
                return this.NotFound();
            }

            return this.View(mainCategory);
        }

        // POST: Administration/MainCategories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainCategory = await this.mainCategoryRepository.All()
                .FirstOrDefaultAsync(mc => mc.Id == id);

            this.mainCategoryRepository.Delete(mainCategory);
            await this.mainCategoryRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> MainCategoryExists(int id)
        {
            return await this.mainCategoryRepository.All().AnyAsync(e => e.Id == id);
        }
    }
}
