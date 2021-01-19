namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    [Area("Administration")]
    public class NewsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsController(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        // GET: Administration/News
        public async Task<IActionResult> Index()
        {
            return this.View(await this.newsRepository.All().ToListAsync());
        }

        public async Task<IActionResult> Restore()
        {
            return this.View(await this.newsRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync());
        }

        [HttpPost]
        [ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var news = await this.newsRepository.AllWithDeleted()
                .FirstOrDefaultAsync(n => n.Id == id);

            news.IsDeleted = false;
            news.DeletedOn = null;
            await this.newsRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var news = await this.newsRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // GET: Administration/News/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (this.ModelState.IsValid)
            {
                await this.newsRepository.AddAsync(news);
                await this.newsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(news);
        }

        // GET: Administration/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var news = await this.newsRepository.All()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // POST: Administration/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (id != news.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.newsRepository.Update(news);
                    await this.newsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.NewsExists(news.Id))
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

            return this.View(news);
        }

        // GET: Administration/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var news = await this.newsRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // POST: Administration/News/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await this.newsRepository.All()
                .FirstOrDefaultAsync(n => n.Id == id);

            this.newsRepository.Delete(news);
            await this.newsRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> NewsExists(int id)
        {
            return await this.newsRepository.All().AnyAsync(e => e.Id == id);
        }
    }
}
