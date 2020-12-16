using Diplom.Models.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Data.StoredProcedures
{
    public static class StoredProcedures
    {
        public static void DeleteOrganization(ApplicationDbContext applicationDbContext, int indexDel)
        {

            var tempOrganization = applicationDbContext.Organizations
                .Where(c => c.Id == indexDel)
                .Select(c => new
                {
                    organizationId = c.Id,
                    employee = c.Employees.Where(o => o.OrganizationId == indexDel),
                    driver = c.Employees.Where(o => o.Driver.Id != null).Select(o => o.Driver),
                    manager = c.Employees.Where(o => o.Manager.Id != null).Select(o => o.Manager),
                    cars = c.Cars
                })
                .FirstOrDefault();

            Organizations org = new Organizations
            {
                Id = tempOrganization.organizationId,
                Employees = tempOrganization.employee.ToList(),
                Cars = tempOrganization.cars
            };

            foreach (Employees emp in org.Employees)
            {

                if (emp.Manager != null)
                {
                    applicationDbContext.Entry(emp.Manager).State = EntityState.Deleted;
                }
                else if (emp.Driver != null)
                {
                    applicationDbContext.Entry(emp.Driver).State = EntityState.Deleted;
                }
                applicationDbContext.Entry(emp).State = EntityState.Deleted;
            }
            foreach (Cars car in org.Cars)
            {
                applicationDbContext.Entry(car).State = EntityState.Deleted;
            }

            applicationDbContext.Entry(org).State = EntityState.Deleted;

            applicationDbContext.SaveChanges();
        }
        public static async void DeleteOrder(ApplicationDbContext applicationDbContext, int indexDel)
        {

        }
    }
}
