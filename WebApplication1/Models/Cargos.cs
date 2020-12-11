namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cargos
    {
        public int Id { get; set; }

        [Required]
        public string LocationLoading { get; set; }

        [Required]
        public string LocationUnloading { get; set; }

        public int Weight { get; set; }

        public int CargoVolum { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [StringLength(450)]
        public string CustomersId { get; set; }

        public int? OrderId { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
