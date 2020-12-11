namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [StringLength(450)]
        public string Id { get; set; }

        public DateTime EmploymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PasportData { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int Salary { get; set; }

        [Required]
        public string Address { get; set; }

        public int OrganizationId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Drivers Drivers { get; set; }

        public virtual Managers Managers { get; set; }

        public virtual Organizations Organizations { get; set; }
    }
}
