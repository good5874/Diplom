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

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders
                .Include(o => o.Car)
                .Include(o => o.Cargo)
                .Include(o => o.Driver.Employees.User)
                .Include(o => o.Manager.Employees.User)
                .Include(o => o.Expenses)
                .Where(e=>e.WasDelivery == true);

            return View( await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> OrdersInProgress()
        {
            var applicationDbContext = _context.Orders
                .Include(o => o.Car)
                .Include(o => o.Cargo)
                .Include(o => o.Driver.Employees.User)
                .Include(o => o.Manager.Employees.User)
                .Include(o => o.Expenses)
                .Where(e=>e.WasPaid == true && e.WasDelivery == false);

            return View(nameof(Index), await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Car)
                .Include(o => o.Cargo)
                .Include(o => o.Driver)
                .Include(o => o.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }
    }
}
