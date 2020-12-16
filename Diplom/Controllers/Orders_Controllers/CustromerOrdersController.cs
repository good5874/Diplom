using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Data;
using Diplom.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Diplom.Data.StoredProcedures;

namespace Diplom.Controllers.Orders_Controllers
{
    [Authorize (Roles = "Customer")]
    public class CustromerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<Users> _userManager;

        public CustromerOrdersController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CustromerOrders
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers
                .Include(e=>e.Orders)
                .Include(e=>e.User)
                .Include(e=>e.Cargos)
                .FirstOrDefaultAsync(c=>c.User.UserName == User.Identity.Name);


            return View(customer.Orders.ToList());
        }

        // GET: CustromerOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(e=>e.Cargo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: CustromerOrders/Create
        public IActionResult Create()
        {
            var customer = _context.Customers
                .Include(e => e.User)
                .Include(e => e.Cargos)
                .FirstOrDefault(e => e.User.UserName == User.Identity.Name);

            ViewData["CargoId"] = new SelectList(customer.Cargos, "Id", "TypeOfCargo");
            return View();
        }

        // POST: CustromerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CargoId,
            [Bind("DateOfTheContract,DateLoading,DateUnloading,LocationLoading,LocationUnloading")] Orders order)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
           .Include(e => e.Customers)
           .Include(e => e.Customers.Cargos)
           .FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);

                var cargo = user.Customers.Cargos.FirstOrDefault(c => c.Id == CargoId);

                order.CustomerId = user.Customers.Id;
                order.Customer = user.Customers;

                cargo.Orders.Add(order);
                order.Cargo = cargo;
                order.CargoId = cargo.Id;

                order.Status = "Order evaluation";


                _context.Add(order);
                _context.Update(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: CustromerOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
           .Include(e => e.Customers)
           .Include(e => e.Customers.Orders)
           .FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);

            var order = user.Customers.Orders.FirstOrDefault(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: CustromerOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,DateOfTheContract,DateLoading,DateUnloading,LocationLoading,LocationUnloading")] Orders order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _order = _context.Orders.FirstOrDefault(c => c.Id == order.Id);
                    _order.DateLoading = order.DateLoading;
                    _order.DateUnloading = order.DateUnloading;
                    _order.LocationLoading = order.LocationLoading;
                    _order.LocationUnloading = order.LocationUnloading;
                    _context.Update(_order);

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
            return View(order);
        }

        // GET: CustromerOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: CustromerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(c => c.Id == id);

            _context.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay (int? id)
        {
            var userId = _userManager.GetUserId(User);

            var order = _context.Orders
                .FirstOrDefault(c => c.Id == id);

            order.WasPaid = true;
            order.Status = "Paid, the manager appoints the driver";

            if (order.CustomerId == userId)
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
