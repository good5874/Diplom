namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Drivers
    {
        [StringLength(450)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DrivingLicense { get; set; }

        public string Location { get; set; }

        public int DrivingExperience { get; set; }

        public virtual Employees Employees { get; set; }
    }
}
