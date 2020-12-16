using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Data;
using Diplom.Models.Tables;
using Microsoft.AspNetCore.Authorization;

namespace Diplom.Controllers.Orders_Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManagerOrders
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders
                .Include(e => e.Manager)
                .Include(e => e.Cargo)
                .Include(e => e.Manager.Employees.User)
                .Include(e => e.Customer.User)
                .Where(c => c.Manager.Employees.User.UserName == User.Identity.Name);

            return View(orders);
        }

        // GET: ManagerOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        public async Task<IActionResult> SetCost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCost(int? id, int Cost)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == id);
            if (order != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        order.Cost = Cost;
                        order.Status = "Awaiting payment";

                        _context.Orders.Update(order);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrdersExists(order.Id))
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
            }

            return View(order);
        }


        public async Task<IActionResult> AppointExecutors(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var user = _context.Users
                .Include(e => e.Employees)
                .Include(e=>e.Employees.Driver)
                .Where(m => m.Employees.Driver != null);

            ViewData["Car"] = new SelectList(_context.Cars, "Id", "CarBrand");
            ViewData["Driver"] = new SelectList(user, "Id", "Email");

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointExecutors(int? id, int? CarId, string driverId)
        {
            if (id == null && CarId == null && driverId == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    order.CarId = CarId;
                    order.DriversId = driverId;
                    order.Status = "Order is in progress";

                    _context.Orders.Update(order);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(order.Id))
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

            return RedirectToAction(nameof(AppointExecutors), id);
        }

        // GET: ManagerOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: ManagerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
