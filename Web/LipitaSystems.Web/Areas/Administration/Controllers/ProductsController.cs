using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LipitaSystems.Data;
using LipitaSystems.Data.Models;

namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext productRepository;

        public ProductsController(ApplicationDbContext context)
        {
            productRepository = context;
        }

        // GET: Administration/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = productRepository.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(productRepository.SecondaryCategories, "Id", "ImageUrl");
            return View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,OriginalPrice,DiscountPercentage,QuantityInStock,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                await productRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(productRepository.SecondaryCategories, "Id", "ImageUrl", product.CategoryId);
            return View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(productRepository.SecondaryCategories, "Id", "ImageUrl", product.CategoryId);
            return View(product);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productRepository.Update(product);
                    await productRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(productRepository.SecondaryCategories, "Id", "ImageUrl", product.CategoryId);
            return View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productRepository.Products.FindAsync(id);
            productRepository.Products.Remove(product);
            await productRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return productRepository.Products.Any(e => e.Id == id);
        }
    }
}
