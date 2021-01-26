namespace LipitaSystems.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Web.ViewModels.ViewModels.Orders;
    using LipitaSystems.Web.ViewModels.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class OrdersController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public OrdersController(IDeletableEntityRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // GET: Administration/Orders
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.orderRepository.All()
                .OrderByDescending(dc => dc.CreatedOn)
                .Select(dc => new AdminAreaOrderViewModel
                {
                    Id = dc.Id,
                    FullName = dc.FullName,
                    PhoneNumber = dc.PhoneNumber,
                    TotalPrice = dc.TotalPrice.ToString("F2"),
                    DeliveryType = dc.DeliveryType,
                    CreatedOn = dc.CreatedOn.AddHours(2).ToString(),
                }).ToListAsync();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Restore()
        {
            return this.View(await this.orderRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync());
        }

        [HttpPost]
        [ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var news = await this.orderRepository.AllWithDeleted()
                .FirstOrDefaultAsync(n => n.Id == id);

            news.IsDeleted = false;
            news.DeletedOn = null;
            await this.orderRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await this.orderRepository.AllWithDeleted()
                .Where(o => o.Id == id)
                .Select(o => new AdminAreaOrderViewModel
                {
                    Id = o.Id,
                    FullName = o.FullName,
                    PhoneNumber = o.PhoneNumber,
                    TotalPrice = o.TotalPrice.ToString("F2"),
                    Address = o.Address,
                    DeliveryType = o.DeliveryType,
                    DeliveryNotes = o.DeliveryNotes != null
                        ? o.DeliveryNotes
                        : "-",
                    DiscountCode = o.DiscountCode != null
                        ? o.DiscountCode.Code
                        : "-",
                    IsDeleted = o.IsDeleted,
                    DeletedOn = o.DeletedOn != null
                        ? o.DeletedOn.ToString()
                        : "-",
                    ModifiedOn = o.ModifiedOn != null
                        ? o.ModifiedOn.ToString()
                        : "-",
                    CreatedOn = o.CreatedOn
                        .AddHours(2)
                        .ToString(),
                    Products = o.Products
                        .Select(p => new ProductForOrderViewModel
                        {
                            Id = p.ProductId,
                            Name = p.Product.Name,
                            Quantity = p.Quantity,
                        }).ToList(),
                }).FirstOrDefaultAsync();

            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        // GET: Administration/Orders/Create
        public IActionResult Create()
        {
            this.ViewData["DeliveryOfficeId"] = new SelectList(this.orderRepository.All(), "Id", "Address");
            return this.View();
        }

        // POST: Administration/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber,TotalPrice,Address,DeliveryType,DeliveryNotes,DeliveryOfficeId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Order order)
        {
            if (this.ModelState.IsValid)
            {
                await this.orderRepository.AddAsync(order);
                await this.orderRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["DeliveryOfficeId"] = new SelectList(this.orderRepository.All(), "Id", "Address", order.DeliveryOfficeId);
            return this.View(order);
        }

        // GET: Administration/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await this.orderRepository.All()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return this.NotFound();
            }

            this.ViewData["DeliveryOfficeId"] = new SelectList(this.orderRepository.All(), "Id", "Address", order.DeliveryOfficeId);
            return this.View(order);
        }

        // POST: Administration/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,PhoneNumber,TotalPrice,Address,DeliveryType,DeliveryNotes,DeliveryOfficeId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Order order)
        {
            if (id != order.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.orderRepository.Update(order);
                    await this.orderRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.OrderExists(order.Id))
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

            this.ViewData["DeliveryOfficeId"] = new SelectList(this.orderRepository.All(), "Id", "Address", order.DeliveryOfficeId);
            return this.View(order);
        }

        // GET: Administration/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await this.orderRepository.All()
                .Include(o => o.DeliveryOffice)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        // POST: Administration/Orders/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await this.orderRepository.All()
                .FirstOrDefaultAsync(o => o.Id == id);

            this.orderRepository.Delete(order);
            await this.orderRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> OrderExists(int id)
        {
            return await this.orderRepository.AllAsNoTracking().AnyAsync(e => e.Id == id);
        }
    }
}
