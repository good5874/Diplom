using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Employees
    {
        public string Id { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string PasportData { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public int OrganizationId { get; set; }

        public virtual Drivers Driver { get; set; }
        public virtual Managers Manager { get; set; }
        public virtual Users User { get; set; }
        public virtual Organizations Organization { get; set; }
    }
}
