namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class DiscountCodesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<DiscountCode> discountCoderRepository;
        private readonly IDeletableEntityRepository<SecondaryCategory> categoryRepository;

        public DiscountCodesController(IDeletableEntityRepository<DiscountCode> discountCoderRepository, IDeletableEntityRepository<SecondaryCategory> categoryRepository)
        {
            this.discountCoderRepository = discountCoderRepository;
            this.categoryRepository = categoryRepository;
        }

        // GET: Administration/DiscountCodes
        public async Task<IActionResult> Index()
        {
            return this.View(await this.discountCoderRepository.All().ToListAsync());
        }

        // GET: Administration/DiscountCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var discountCode = await this.discountCoderRepository.All()
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
            this.ViewData["CategoriesId"] = new SelectList(this.categoryRepository.All(), "Id", "Name");
            return this.View();
        }

        // POST: Administration/DiscountCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,DiscountPercentage,DoesWorkOnDiscountedProducts,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] DiscountCode discountCode)
        {
            if (this.ModelState.IsValid)
            {
                await this.discountCoderRepository.AddAsync(discountCode);
                await this.discountCoderRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoriesId"] = new SelectList(this.categoryRepository.All(), "Id", "Name");
            return this.View(discountCode);
        }

        // GET: Administration/DiscountCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var discountCode = await this.discountCoderRepository.All()
                .FirstOrDefaultAsync(dc => dc.Id == id);
            if (discountCode == null)
            {
                return this.NotFound();
            }

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
                    this.discountCoderRepository.Update(discountCode);
                    await this.discountCoderRepository.SaveChangesAsync();
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

            var discountCode = await this.discountCoderRepository.All()
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
            var discountCode = await this.discountCoderRepository.All().FirstOrDefaultAsync(dc => dc.Id == id);
            this.discountCoderRepository.Delete(discountCode);
            await this.discountCoderRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> DiscountCodeExists(int id)
        {
            return await this.discountCoderRepository.AllAsNoTracking().AnyAsync(e => e.Id == id);
        }
    }
}
