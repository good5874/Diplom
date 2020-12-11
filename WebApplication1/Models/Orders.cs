namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Cargos = new HashSet<Cargos>();
        }

        public int Id { get; set; }

        [Required]
        public string DateOfTheContract { get; set; }

        public int Cost { get; set; }

        public bool WasPaid { get; set; }

        public bool WasDelivery { get; set; }

        public int CargoId { get; set; }

        [Required]
        [StringLength(450)]
        public string CustomerId { get; set; }

        [StringLength(450)]
        public string ManagerId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cargos> Cargos { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Managers Managers { get; set; }
    }
}
