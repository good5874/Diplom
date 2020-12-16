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

namespace Diplom.Controllers
{

    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<Users> _userManager;
        public EmployeesController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Employees
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users
                .Include(e => e.Employees)
                .Include(e => e.Employees.Driver)
                .Include(e => e.Employees.Manager)
                .Include(e => e.Employees.Organization)
                .Where(e=>e.Employees != null);

            List<Users> tmpList = new List<Users>();
            foreach(var user in applicationDbContext)
            {
                if(await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    tmpList.Add(user);
                }
                else if (await _userManager.IsInRoleAsync(user, "Driver"))
                {
                    tmpList.Add(user);
                }
            }

            return View(tmpList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Vacancies()
        {
            var applicationDbContext = _context.Users
                .Include(e => e.Employees)
                .Include(e => e.Employees.Driver)
                .Include(e => e.Employees.Manager)
                .Include(e => e.Employees.Organization)
                .Where(e => e.Employees != null);

            List<Users> tmpList = new List<Users>();
            foreach (var user in applicationDbContext)
            {
                if (!await _userManager.IsInRoleAsync(user, "Manager"))
                {

                    if (!await _userManager.IsInRoleAsync(user, "Driver"))
                    {
                        tmpList.Add(user);
                    }
                }
            }

            return View(nameof(Index), tmpList);
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
        public IActionResult Create(string titleJob)
        {
            var id = _userManager.GetUserId(User);
            var user = _context.Users
                .Include(e => e.Employees)
                .Include(e => e.Employees.Driver)
                .Include(e => e.Employees.Manager)
                .Include(e => e.Employees.Organization)
                .FirstOrDefaultAsync(m => m.Id == id).Result;

            if (user.Employees != null)
            {
                return RedirectToRoute("default", new { controller = "Employees", action = "Details", user.Id });
            }
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address");
            if (titleJob == "driver")
            {
                return View(nameof(CreateDriver));
            }
            else if (titleJob == "manager")
            {
                return View(nameof(CreateManager));
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateManager([Bind("EmploymentDate,PasportData,DateOfBirth,Salary,Address,OrganizationId")] Employees employees,
             [Bind("Education")] Managers manager)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    employees.Id = user.Id;
                    _context.Add(employees);
                    manager.Id = user.Id;
                    _context.Add(manager);

                    await _context.SaveChangesAsync();
                    return RedirectToRoute("default", new { controller = "Employees", action = "Details", user.Id });
                }
            }
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", employees.Id);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address", employees.OrganizationId);
            return Redirect("~/Identity/Account/Manage/Index");
        }

        public async Task<IActionResult> CreateDriver([Bind("EmploymentDate,PasportData,DateOfBirth,Salary,Address,OrganizationId")] Employees employees,
           [Bind("DrivingLicense,DrivingExperience")] Drivers driver)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    employees.Id = user.Id;
                    _context.Add(employees);
                    driver.Id = user.Id;
                    _context.Add(driver);

                    await _context.SaveChangesAsync();
                    return RedirectToRoute("default", new { controller = "Employees", action = "Details", user.Id });
                }
            }
            ViewData["Id"] = new SelectList(_context.Drivers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Managers, "Id", "Id", employees.Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", employees.Id);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Address", employees.OrganizationId);
            return Redirect("~/Identity/Account/Manage/Index");
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
            var employees = await _context.Employees
                .Include(e=>e.Driver)
                .Include(e=>e.Manager)
                .FirstOrDefaultAsync(m=>m.Id == id);
            if (employees.Driver != null)
            {
                _context.Drivers.Remove(employees.Driver);
                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
            }
            if (employees.Manager != null)
            {
                _context.Managers.Remove(employees.Manager);
                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
            }
            return Redirect("~/Identity/Account/Manage/Index");
        }

        private bool EmployeesExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
