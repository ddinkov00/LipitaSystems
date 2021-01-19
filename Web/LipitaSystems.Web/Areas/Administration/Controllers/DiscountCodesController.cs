namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class DiscountCodesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<DiscountCode> discountCodeRepository;
        private readonly IDeletableEntityRepository<SecondaryCategory> secondaryCodeRepository;
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public DiscountCodesController(
            IDeletableEntityRepository<DiscountCode> discountCodeRepository,
            IDeletableEntityRepository<SecondaryCategory> secondaryCodeRepository,
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.discountCodeRepository = discountCodeRepository;
            this.secondaryCodeRepository = secondaryCodeRepository;
            this.mainCategoryRepository = mainCategoryRepository;
        }

        // GET: Administration/DiscountCodes
        public async Task<IActionResult> Index()
        {
            return this.View(await this.discountCodeRepository.All()
                .ToListAsync());
        }

        public async Task<IActionResult> Restore()
        {
            return this.View(await this.discountCodeRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync());
        }

        [HttpPost]
        [ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var news = await this.discountCodeRepository.AllWithDeleted()
                .FirstOrDefaultAsync(n => n.Id == id);

            news.IsDeleted = false;
            news.DeletedOn = null;
            await this.discountCodeRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/DiscountCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var discountCode = await this.discountCodeRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (discountCode == null)
            {
                return this.NotFound();
            }

            return this.View(discountCode);
        }

        // GET: Administration/DiscountCodes/Create
        public IActionResult Create()
        {
            this.ViewData["MainCategories"] = new SelectList(this.mainCategoryRepository.All(), "Id", "Name");
            return this.View();
        }

        // POST: Administration/DiscountCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,DiscountPercentage,DoesWorkOnDiscountedProducts,MainCategoryId")] DiscountCodeInputModel discountCode)
        {
            if (this.ModelState.IsValid)
            {
                var discountCodeToAdd = new DiscountCode
                {
                    Code = discountCode.Code,
                    DiscountPercentage = discountCode.DiscountPercentage,
                    DoesWorkOnDiscountedProducts = discountCode.DoesWorkOnDiscountedProducts,
                };

                await this.discountCodeRepository.AddAsync(discountCodeToAdd);
                await this.discountCodeRepository.SaveChangesAsync();

                var secondaryCategories = this.secondaryCodeRepository.All()
                    .Where(sc => sc.MainCategoryId == discountCode.MainCategoryId);

                foreach (var secondaryCategory in secondaryCategories)
                {
                    secondaryCategory.DiscountCodes.Add(discountCodeToAdd);
                }

                await this.secondaryCodeRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["MainCategories"] = new SelectList(this.mainCategoryRepository.All(), "Id", "Name", discountCode.MainCategoryId);
            return this.View(discountCode);
        }

        // GET: Administration/DiscountCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var discountCode = await this.discountCodeRepository.All()
                .FirstOrDefaultAsync(dc => dc.Id == id);

            if (discountCode == null)
            {
                return this.NotFound();
            }

            this.ViewData["SecondaryCategories"] = new SelectList(this.secondaryCodeRepository.All(), "Id", "Name", discountCode.SecondaryCategories.Select(x => x.Id));
            return this.View(discountCode);
        }

        // POST: Administration/DiscountCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,DiscountPercentage,DoesWorkOnDiscountedProducts,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] DiscountCode discountCode)
        {
            if (id != discountCode.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.discountCodeRepository.Update(discountCode);
                    await this.discountCodeRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.DiscountCodeExists(discountCode.Id))
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

            return this.View(discountCode);
        }

        // GET: Administration/DiscountCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var discountCode = await this.discountCodeRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (discountCode == null)
            {
                return this.NotFound();
            }

            return this.View(discountCode);
        }

        // POST: Administration/DiscountCodes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountCode = await this.discountCodeRepository.All()
                .FirstOrDefaultAsync(dc => dc.Id == id);

            this.discountCodeRepository.Delete(discountCode);
            await this.discountCodeRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> DiscountCodeExists(int id)
        {
            return await this.discountCodeRepository.All().AnyAsync(e => e.Id == id);
        }
    }
}
