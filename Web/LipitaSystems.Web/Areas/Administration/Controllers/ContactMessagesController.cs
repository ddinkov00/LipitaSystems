namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ContactMessagesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<ContactMessage> contactMessageRepository;

        public ContactMessagesController(IDeletableEntityRepository<ContactMessage> deletableEntityRepository)
        {
            this.contactMessageRepository = deletableEntityRepository;
        }

        // GET: Administration/ContactMessages
        public async Task<IActionResult> Index()
        {
            return this.View(await this.contactMessageRepository.AllAsNoTracking().ToListAsync());
        }

        // GET: Administration/ContactMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactMessage = await this.contactMessageRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactMessage == null)
            {
                return this.NotFound();
            }

            return this.View(contactMessage);
        }

        // GET: Administration/ContactMessages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/ContactMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Contact,Subject,Message,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] ContactMessage contactMessage)
        {
            if (this.ModelState.IsValid)
            {
                await this.contactMessageRepository.AddAsync(contactMessage);
                await this.contactMessageRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(contactMessage);
        }

        // GET: Administration/ContactMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactMessage = await this.contactMessageRepository.All()
                .FirstOrDefaultAsync(cm => cm.Id == id);

            if (contactMessage == null)
            {
                return this.NotFound();
            }

            return this.View(contactMessage);
        }

        // POST: Administration/ContactMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Contact,Subject,Message,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] ContactMessage contactMessage)
        {
            if (id != contactMessage.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.contactMessageRepository.Update(contactMessage);
                    await this.contactMessageRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.ContactMessageExists(contactMessage.Id))
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

            return this.View(contactMessage);
        }

        // GET: Administration/ContactMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactMessage = await this.contactMessageRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contactMessage == null)
            {
                return this.NotFound();
            }

            return this.View(contactMessage);
        }

        // POST: Administration/ContactMessages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactMessage = await this.contactMessageRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(cm => cm.Id == id);

            this.contactMessageRepository.Delete(contactMessage);
            await this.contactMessageRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> ContactMessageExists(int id)
        {
            return await this.contactMessageRepository.AllAsNoTracking().AnyAsync(e => e.Id == id);
        }
    }
}
