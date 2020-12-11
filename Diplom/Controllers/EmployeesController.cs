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

namespace Diplom.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<Users> _userManager;
        public EmployeesController(ApplicationDbContext context, UserManager<Users> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.Driver).Include(e => e.Manager).Include(e => e.User).Include(e => e.Organization);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Driver)
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmploymentDate,PasportData,DateOfBirth,Salary,Address,OrganizationId")] Employees employees,
            [Bind ("Id,DrivingLicense,DrivingExperience")] Drivers driver)
        {
            if (ModelState.IsValid)
            {
                if(User.Identity.IsAuthenticated)
                {
                    string id = _userManager.GetUserId(User);
                    employees.Id = id;
                    driver.Id = id;
                    _context.Add(employees);
                    _context.Add(driver);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", employees.Id);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address", employees.OrganizationId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", employees.Id);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address", employees.OrganizationId);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,EmploymentDate,PasportData,DateOfBirth,Salary,Address,OrganizationId")] Employees employees)
        {
            if (id != employees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.Id))
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
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", employees.Id);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address", employees.OrganizationId);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Driver)
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
