﻿using System;
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
    public class SecondaryCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondaryCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/SecondaryCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SecondaryCategories.Include(s => s.MainCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/SecondaryCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryCategory = await _context.SecondaryCategories
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryCategory == null)
            {
                return NotFound();
            }

            return View(secondaryCategory);
        }

        // GET: Administration/SecondaryCategories/Create
        public IActionResult Create()
        {
            ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "ImageUrl");
            return View();
        }

        // POST: Administration/SecondaryCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ImageUrl,MainCategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] SecondaryCategory secondaryCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secondaryCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "ImageUrl", secondaryCategory.MainCategoryId);
            return View(secondaryCategory);
        }

        // GET: Administration/SecondaryCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryCategory = await _context.SecondaryCategories.FindAsync(id);
            if (secondaryCategory == null)
            {
                return NotFound();
            }
            ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "ImageUrl", secondaryCategory.MainCategoryId);
            return View(secondaryCategory);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondaryCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondaryCategoryExists(secondaryCategory.Id))
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
            ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "ImageUrl", secondaryCategory.MainCategoryId);
            return View(secondaryCategory);
        }

        // GET: Administration/SecondaryCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryCategory = await _context.SecondaryCategories
                .Include(s => s.MainCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryCategory == null)
            {
                return NotFound();
            }

            return View(secondaryCategory);
        }

        // POST: Administration/SecondaryCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secondaryCategory = await _context.SecondaryCategories.FindAsync(id);
            _context.SecondaryCategories.Remove(secondaryCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondaryCategoryExists(int id)
        {
            return _context.SecondaryCategories.Any(e => e.Id == id);
        }
    }
}
