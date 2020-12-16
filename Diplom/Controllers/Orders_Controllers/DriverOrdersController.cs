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
using Microsoft.AspNetCore.Identity;

namespace Diplom.Controllers.Orders_Controllers
{
    [Authorize(Roles = "Driver")]
    public class DriverOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<Users> _userManager;

        public DriverOrdersController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DriverOrders
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var applicationDbContext = _context.Orders
                .Include(o => o.Car)
                .Include(o => o.Cargo)
                .Where(m => m.DriversId == userId && m.WasDelivery == false);

            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Report(int? id)
        {
            var expenses = _context.Expenses
                .FirstOrDefault(m => m.Id == id);

            return View(expenses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(int? id, int fuelCosts, int otherExpenses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders
               .Include(o => o.Expenses)
               .FirstOrDefault(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = "performed";
            order.WasDelivery = true;
            order.Expenses = new Expenses()
            {
                FuelCosts = fuelCosts,
                OtherExpenses = otherExpenses
            };
            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
