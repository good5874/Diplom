using System;
using System.Collections.Generic;

namespace Diplom.Models.Tables
{
    public partial class Orders
    {
        public Orders()
        {
            Cargos = new HashSet<Cargos>();
        }

        public int Id { get; set; }
        public string DateOfTheContract { get; set; }
        public int Cost { get; set; }
        public bool WasPaid { get; set; }
        public bool WasDelivery { get; set; }
        public int CargoId { get; set; }
        public string CustomerId { get; set; }
        public string ManagerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Managers Manager { get; set; }
        public virtual ICollection<Cargos> Cargos { get; set; }
    }
}
