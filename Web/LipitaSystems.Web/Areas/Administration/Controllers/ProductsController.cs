namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ProductsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<SecondaryCategory> secondaryRepository;
        private readonly IImageService imageService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public ProductsController(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<SecondaryCategory> secondaryRepository,
            IImageService imageService,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary)
        {
            this.productRepository = productRepository;
            this.secondaryRepository = secondaryRepository;
            this.imageService = imageService;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
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

            var product = await this.productRepository.AllWithDeleted()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        public async Task<IActionResult> Restore()
        {
            return this.View(await this.productRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync());
        }

        [HttpPost]
        [ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var news = await this.productRepository.AllWithDeleted()
                .FirstOrDefaultAsync(n => n.Id == id);

            news.IsDeleted = false;
            news.DeletedOn = null;
            await this.productRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(
                this.secondaryRepository.All()
                    .OrderBy(p => p.Name),
                "Id",
                "Name");

            return this.View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Images,QuantityInstock,SecondaryCategoryId")] ProductInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var imageUrls = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Images);

                var product = new Product
                {
                    Name = input.Name,
                    Description = input.Description,
                    OriginalPrice = input.Price,
                    QuantityInStock = input.QuantityInstock,
                    CategoryId = input.SecondaryCategoryId,
                };

                await this.productRepository.AddAsync(product);
                await this.productRepository.SaveChangesAsync();

                var productId = product.Id;

                foreach (var imageUrl in imageUrls)
                {
                    await this.imageService.CreateAsync(imageUrl, productId);
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.secondaryRepository.All(), "Id", "Name", input.SecondaryCategoryId);
            return this.View(input);
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

            this.ViewData["CategoryId"] = new SelectList(this.secondaryRepository.All(), "Id", "Name", product.CategoryId);
            return this.View(product);
        }

        // POST: Administration/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,OriginalPrice,DiscountPercentage,QuantityInStock,CategoryId,Id")] Product product)
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

            this.ViewData["CategoryId"] = new SelectList(this.secondaryRepository.All(), "Id", "Name", product.CategoryId);
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
