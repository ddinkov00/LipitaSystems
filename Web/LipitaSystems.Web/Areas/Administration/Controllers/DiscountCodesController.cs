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
    public class DiscountCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscountCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/DiscountCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountCodes.ToListAsync());
        }

        // GET: Administration/DiscountCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCode = await _context.DiscountCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCode == null)
            {
                return NotFound();
            }

            return View(discountCode);
        }

        // GET: Administration/DiscountCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/DiscountCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,DiscountPercentage,DoesWorkOnDiscountedProducts,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] DiscountCode discountCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCode);
        }

        // GET: Administration/DiscountCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCode = await _context.DiscountCodes.FindAsync(id);
            if (discountCode == null)
            {
                return NotFound();
            }
            return View(discountCode);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCodeExists(discountCode.Id))
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
            return View(discountCode);
        }

        // GET: Administration/DiscountCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCode = await _context.DiscountCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCode == null)
            {
                return NotFound();
            }

            return View(discountCode);
        }

        // POST: Administration/DiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountCode = await _context.DiscountCodes.FindAsync(id);
            _context.DiscountCodes.Remove(discountCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCodeExists(int id)
        {
            return _context.DiscountCodes.Any(e => e.Id == id);
        }
    }
}
