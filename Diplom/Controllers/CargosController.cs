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
using Diplom.Data.StoredProcedures;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CargosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cargos
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Cargos)
                .FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);

            return View(customers.Cargos.ToList());
        }

        // GET: Cargos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargos = await _context.Cargos
                .Include(c => c.Customers)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargos == null)
            {
                return NotFound();
            }

            return View(cargos);
        }

        // GET: Cargos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,CargoVolume,TypeOfCargo")] Cargos cargos)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
          .Include(e => e.Customers)
          .FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);

                cargos.CustomersId = user.Id;
                cargos.Customers = user.Customers;
                _context.Add(cargos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargos);
        }

        // GET: Cargos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargos = await _context.Cargos.FindAsync(id);
            if (cargos == null)
            {
                return NotFound();
            }
            return View(cargos);
        }

        // POST: Cargos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("Id,Weight,CargoVolume,TypeOfCargo")] Cargos cargos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _cargo = _context.Cargos.Where(c => c.Id == cargos.Id).FirstOrDefault();
                    _cargo.TypeOfCargo = cargos.TypeOfCargo;
                    _cargo.Weight = cargos.Weight;
                    _cargo.CargoVolume = cargos.CargoVolume;
                    _context.Update(_cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargosExists(cargos.Id))
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
            return View(cargos);
        }

        // GET: Cargos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargos = await _context.Cargos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargos == null)
            {
                return NotFound();
            }

            return View(cargos);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargos
                .FirstOrDefaultAsync(c => c.Id == id);

            _context.Remove(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargosExists(int id)
        {
            return _context.Cargos.Any(e => e.Id == id);
        }
    }
}
