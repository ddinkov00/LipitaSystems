namespace LipitaSystems.Web.Areas.Administration
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class DiscountCodesController : Controller
    {
        private readonly IDeletableEntityRepository<DiscountCode> discountCodeRepository;

        public DiscountCodesController(IDeletableEntityRepository<DiscountCode> discountCodeRepository)
        {
            this.discountCodeRepository = discountCodeRepository;
        }

        // GET: Administration/DiscountCodes
        public async Task<IActionResult> Index()
        {
            return this.View(await this.discountCodeRepository.All()
                .ToListAsync());
        }

        // GET: Administration/DiscountCodes/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Administration/DiscountCodes/Create
        public IActionResult Create()
        {
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
                await this.discountCodeRepository.AddAsync(discountCode);
                await this.discountCodeRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

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
