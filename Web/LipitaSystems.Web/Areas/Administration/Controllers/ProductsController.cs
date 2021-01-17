namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ProductsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsController(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Administration/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.productRepository.All()
                .Include(p => p.Category);

            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productRepository.All()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this.productRepository.All(), "Id", "ImageUrl");
            return this.View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,OriginalPrice,DiscountPercentage,QuantityInStock,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (this.ModelState.IsValid)
            {
                await this.productRepository.AddAsync(product);
                await this.productRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.productRepository.All(), "Id", "ImageUrl", product.CategoryId);
            return this.View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productRepository.All().
                FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this.productRepository.All(), "Id", "ImageUrl", product.CategoryId);
            return this.View(product);
        }

        // POST: Administration/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,OriginalPrice,DiscountPercentage,QuantityInStock,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (id != product.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.productRepository.Update(product);
                    await this.productRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.ProductExists(product.Id))
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

            this.ViewData["CategoryId"] = new SelectList(this.productRepository.All(), "Id", "ImageUrl", product.CategoryId);
            return this.View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productRepository.All()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.productRepository.All()
                .FirstOrDefaultAsync(p => p.Id == id);

            this.productRepository.Delete(product);
            await this.productRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            return await this.productRepository.All().AnyAsync(e => e.Id == id);
        }
    }
}
