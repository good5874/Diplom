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
    [Authorize(Roles = "Manager")]
    public class StoreOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<Users> _userManager;

        public StoreOrdersController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ManagerOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders
                .Include(o => o.Customer.User)
                .Include(o => o.Manager)
                .Include(o => o.Cargo)
                .Where(o=>o.Manager == null);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PickUpTheOrder(int? id)
        {
            var orders = _context.Orders
                .Include(o => o.Customer.User)
                .Include(o => o.Manager)
                .Include(o => o.Cargo)
                .Where(o=>o.Manager == null);
            if(id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userId = _userManager.GetUserId(User);

            var order = orders.FirstOrDefault(c => c.Id == id);
            order.ManagerId = userId;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
