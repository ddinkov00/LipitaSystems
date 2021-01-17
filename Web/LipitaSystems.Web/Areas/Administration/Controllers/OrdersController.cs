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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext orderRepository;

        public OrdersController(ApplicationDbContext context)
        {
            orderRepository = context;
        }

        // GET: Administration/Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = orderRepository.Orders.Include(o => o.DeliveryOffice);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await orderRepository.Orders
                .Include(o => o.DeliveryOffice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        // GET: Administration/Orders/Create
        public IActionResult Create()
        {
            this.ViewData["DeliveryOfficeId"] = new SelectList(orderRepository.DeliveryOffices, "Id", "Address");
            return this.View();
        }

        // POST: Administration/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber,TotalPrice,Address,DeliveryType,DeliveryNotes,DeliveryOfficeId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.Add(order);
                await orderRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(Index));
            }
            this.ViewData["DeliveryOfficeId"] = new SelectList(orderRepository.DeliveryOffices, "Id", "Address", order.DeliveryOfficeId);
            return this.View(order);
        }

        // GET: Administration/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await orderRepository.Orders.FindAsync(id);
            if (order == null)
            {
                return this.NotFound();
            }
            this.ViewData["DeliveryOfficeId"] = new SelectList(orderRepository.DeliveryOffices, "Id", "Address", order.DeliveryOfficeId);
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

            if (ModelState.IsValid)
            {
                try
                {
                    orderRepository.Update(order);
                    await orderRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            this.ViewData["DeliveryOfficeId"] = new SelectList(orderRepository.DeliveryOffices, "Id", "Address", order.DeliveryOfficeId);
            return this.View(order);
        }

        // GET: Administration/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = await orderRepository.Orders
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
            var order = await orderRepository.Orders.FindAsync(id);
            orderRepository.Orders.Remove(order);
            await orderRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool OrderExists(int id)
        {
            return orderRepository.Orders.Any(e => e.Id == id);
        }
    }
}
