namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using LipitaSystems.Data;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services;
    using LipitaSystems.Web.Areas.Administration.Controllers;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class FrontImagesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<FrontImage> frontImageRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public FrontImagesController(IDeletableEntityRepository<FrontImage> frontImageRepository,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary)
        {
            this.frontImageRepository = frontImageRepository;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
        }

        // GET: FrontImages
        public async Task<IActionResult> Index()
        {
            return this.View(await this.frontImageRepository.All().ToListAsync());
        }

        public async Task<IActionResult> Restore()
        {
            return this.View(await this.frontImageRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync());
        }

        [HttpPost]
        [ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var mainCat = await this.frontImageRepository.AllWithDeleted()
                .FirstOrDefaultAsync(n => n.Id == id);

            mainCat.IsDeleted = false;
            mainCat.DeletedOn = null;
            await this.frontImageRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: FrontImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var frontImage = await this.frontImageRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frontImage == null)
            {
                return this.NotFound();
            }

            return this.View(frontImage);
        }

        // GET: FrontImages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: FrontImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Heading, Content, Image, RedirectUrl, Order")] FrontImageInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var imageUrl = await this.cloudinaryService.UploadAsyncSingle(this.cloudinary, input.Image);

                var frontImage = new FrontImage
                {
                    Heading = input.Heading,
                    ImgUrl = imageUrl,
                    Content = input.Content,
                    RedirectUrl = input.RedirectUrl,
                    Order = input.Order,
                };

                await this.frontImageRepository.AddAsync(frontImage);
                await this.frontImageRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View();
        }

        // GET: FrontImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var frontImage = await this.frontImageRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (frontImage == null)
            {
                return this.NotFound();
            }

            return this.View(frontImage);
        }

        // POST: FrontImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImgUrl,Heading,Content,RedirectUrl,Order,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] FrontImage frontImage)
        {
            if (id != frontImage.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.frontImageRepository.Update(frontImage);
                    await this.frontImageRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.FrontImageExists(frontImage.Id))
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

            return this.View(frontImage);
        }

        // GET: FrontImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var frontImage = await this.frontImageRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frontImage == null)
            {
                return this.NotFound();
            }

            return this.View(frontImage);
        }

        // POST: FrontImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var frontImage = await this.frontImageRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.frontImageRepository.Delete(frontImage);
            await this.frontImageRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> FrontImageExists(int id)
        {
            return await this.frontImageRepository.AllWithDeleted().AnyAsync(e => e.Id == id);
        }
    }
}
